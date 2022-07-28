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
    public class DeleteContactCommand : BaseCommandAsync
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

        public override async Task ExecuteAsync(object? parameter)
        {
            var contact = _contactViewModel.SelectedContact;
            _contactViewModel.Contacts.Remove(contact);
            await Task.Run(() => DeleteContact(contact));
        }

        private void DeleteContact(Contact contact) 
        {
            using (DataContext db = new())
            {
                var contactDb = db.Contacts?.FirstOrDefault(x => x.ContactId == contact.ContactId);
                if (contactDb != null)
                {
                    db.Contacts?.Remove(contactDb);
                    db.SaveChanges();
                }
            }
        }
    }
}