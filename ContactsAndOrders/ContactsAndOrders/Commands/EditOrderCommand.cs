using ContactsAndOrders.ViewModels;
using System.ComponentModel;

namespace ContactsAndOrders.Commands
{
    public class EditOrderCommand : BaseCommand
    {
        private readonly OrderViewModel _orderViewModel;

        public EditOrderCommand(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _orderViewModel.PropertyChanged += OrderViewModelPropertyChanged;
        }

        private void OrderViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.SelectedOrder)
                || e.PropertyName == nameof(OrderViewModel.Editable))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _orderViewModel.SelectedOrder != null;
        }

        public override void Execute(object? parameter)
        {
            _orderViewModel.Editable = true;
        }
    }
}