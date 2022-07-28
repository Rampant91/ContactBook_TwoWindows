using ContactBook_TwoWindows.DbRealization;
using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using ContactsAndOrders.Commands.Async;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBook_TwoWindows.Commands
{
    public class AddContactCommand : BaseCommandAsync
    {
        private readonly ContactViewModel _contactViewModel;

        public AddContactCommand(ContactViewModel contactsViewModel)
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
            return true;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Contact contact = new();
            
            _contactViewModel.Editable = true;
            await Task.Run(() => AddContact(contact));
            _contactViewModel.SelectedContact = contact;
            _contactViewModel.Contacts.Add(contact);
        }

        private static void AddContact(Contact contact)
        {
            using (DataContext db = new())
            {
                db.Contacts?.Add(contact);
                db.SaveChanges();
            }
        }
    }
}