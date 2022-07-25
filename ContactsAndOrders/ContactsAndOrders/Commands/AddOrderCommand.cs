using ContactsAndOrders.DbRealization;
using ContactsAndOrders.Models;
using ContactsAndOrders.ViewModels;
using System.ComponentModel;

namespace ContactsAndOrders.Commands
{
    public class AddOrderCommand : BaseCommand
    {
        private OrderViewModel _orderViewModel;

        public AddOrderCommand(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _orderViewModel.PropertyChanged += OrderViewModelPropertyChanged;
        }

        private void OrderViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.Orders))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            using (DataContext db = new DataContext())
            {
                Order order = new Order();
                order.ContactId = _orderViewModel.ContactId;
                db.Orders.Add(order);
                _orderViewModel.SelectedOrder = order;
                _orderViewModel.Editable = true;
                _orderViewModel.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}