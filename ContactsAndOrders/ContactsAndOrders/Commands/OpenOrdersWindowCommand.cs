using ContactsAndOrders.DbRealization;
using ContactsAndOrders.ViewModels;
using ContactsAndOrders.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ContactsAndOrders.Commands
{
    public class OpenOrdersWindowCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public OpenOrdersWindowCommand(ContactViewModel contactViewMode)
        {
            _contactViewModel = contactViewMode;
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
            Window _ordersWindow = new OrdersWindow()
            {
                DataContext = _contactViewModel
            };
            _ordersWindow.Show();
        }
    }
}