using ContactsAndOrders.DbRealization;
using ContactsAndOrders.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace ContactsAndOrders.Commands
{
    public class DeleteContactCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public DeleteContactCommand(ContactViewModel contactsViewModel)
        {
            _contactViewModel = contactsViewModel;
            _contactViewModel.PropertyChanged += ContactViewModelPropertyChanged;
        }
        private void ContactViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactViewModel.SelectedContact))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _contactViewModel.SelectedContact != null;
        }

        public override void Execute(object? parameter)
        {
            var contact = _contactViewModel.SelectedContact;
            using (DataContext db = new DataContext())
            {
                var contactDb = db.Contacts.FirstOrDefault(x => x.ContactId == contact.ContactId);
                if (contactDb != null)
                {
                    db.Contacts.Remove(contactDb);
                    _contactViewModel.Contacts.Remove(contact);
                    db.SaveChanges();
                }
            }
        }
    }
}