using ContactBook_TwoWindows.ViewModels;
using System.ComponentModel;

namespace ContactBook_TwoWindows.Commands
{
    public class EditContactCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public EditContactCommand(ContactViewModel contactsViewModel)
        {
            _contactViewModel = contactsViewModel;
            _contactViewModel.PropertyChanged += ContactViewModelPropertyChanged;
        }

        private void ContactViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactViewModel.SelectedContact)
                || e.PropertyName == nameof(ContactViewModel.SelectedContact.Editable))
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
            _contactViewModel.Editable = true;
        }
    }
}