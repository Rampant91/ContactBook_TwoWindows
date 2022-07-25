using ContactBook_TwoWindows.DbRealization;
using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using System.ComponentModel;

namespace ContactBook_TwoWindows.Commands
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