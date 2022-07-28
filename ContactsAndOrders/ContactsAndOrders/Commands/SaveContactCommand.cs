using ContactBook_TwoWindows.DbRealization;
using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using ContactsAndOrders.Commands.Async;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBook_TwoWindows.Commands
{
    public class SaveContactCommand : BaseCommandAsync
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

        public override async Task ExecuteAsync(object? parameter)
        {
            Contact contact = _contactViewModel.SelectedContact!;
            _contactViewModel.Editable = false;
            await Task.Run(() => SaveContact(contact));
        }

        private void SaveContact(Contact contact)
        {
            using (DataContext db = new DataContext())
            {
                var contactDb = db.Contacts?.FirstOrDefault(x => x.ContactId == contact.ContactId);
                if (contactDb != null)
                {
                    contactDb.FirstName = contact.FirstName;
                    contactDb.LastName = contact.LastName;
                    contactDb.Patronymic = contact.Patronymic;
                    contactDb.City = contact.City;
                    contactDb.Address = contact.Address;
                    contactDb.Phone = contact.Phone;
                    contactDb.Email = contact.Email;
                    Thread.Sleep(5000);
                    db.SaveChanges();
                }
            }
        }
    }
}