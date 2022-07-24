using ContactsAndOrders.DbRealization;
using ContactsAndOrders.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace ContactsAndOrders.Commands
{
    public class SaveContactCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public SaveContactCommand(ContactViewModel contactViewMode)
        {
            _contactViewModel = contactViewMode;
            _contactViewModel.PropertyChanged += ContactViewModelPropertyChanged;
        }

        private void ContactViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactViewModel.SelectedContact)
                || e.PropertyName == nameof(ContactViewModel.FirstName)
                || e.PropertyName == nameof(ContactViewModel.LastName)
                || e.PropertyName == nameof(ContactViewModel.Patronymic)
                || e.PropertyName == nameof(ContactViewModel.City)
                || e.PropertyName == nameof(ContactViewModel.Address)
                || e.PropertyName == nameof(ContactViewModel.Phone)
                || e.PropertyName == nameof(ContactViewModel.Email)
                || e.PropertyName == nameof(ContactViewModel.Editable))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _contactViewModel.SelectedContact != null && _contactViewModel.Editable == true;
        }

        public override void Execute(object? parameter)
        {
            using (DataContext db = new DataContext())
            {
                var contact = _contactViewModel.SelectedContact;
                var contactDb = db.Contacts.FirstOrDefault(x => x.ContactId == contact.ContactId);
                if (contactDb != null)
                {
                    contactDb.FirstName = contact.FirstName;
                    contactDb.LastName = contact.LastName;
                    contactDb.Patronymic = contact.Patronymic;
                    contactDb.City = contact.City;
                    contactDb.Address = contact.Address;
                    contactDb.Phone = contact.Phone;
                    contactDb.Email = contact.Email;
                    db.SaveChanges();
                }
                _contactViewModel.Editable = false;
            }
        }
    }
}