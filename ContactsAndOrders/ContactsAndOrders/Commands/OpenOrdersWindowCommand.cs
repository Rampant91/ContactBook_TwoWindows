using ContactBook_TwoWindows.ViewModels;
using ContactBook_TwoWindows.Windows;
using System.ComponentModel;
using System.Windows;

namespace ContactBook_TwoWindows.Commands
{
    public class OpenOrdersWindowCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public OpenOrdersWindowCommand(ContactViewModel contactViewModel)
        {
            _contactViewModel = contactViewModel;
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
            OrdersWindow ordersWindow = new OrdersWindow()
            {
                DataContext = new OrderViewModel(_contactViewModel.SelectedContact!.ContactId)
            };
            ordersWindow.ShowDialog();
        }
    }
}