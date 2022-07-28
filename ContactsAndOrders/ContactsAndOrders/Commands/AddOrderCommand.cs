using ContactBook_TwoWindows.DbRealization;
using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using ContactsAndOrders.Commands.Async;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBook_TwoWindows.Commands
{
    public class AddOrderCommand : BaseCommandAsync
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

        public override async Task ExecuteAsync(object? parameter)
        {
            Order order = new();
            order.ContactId = _orderViewModel.ContactId;
            _orderViewModel.SelectedOrder = order;
            _orderViewModel.Editable = true;
            _orderViewModel.Orders.Add(order);
            await Task.Run(() => AddOrder(order));
        }

        private static void AddOrder(Order order)
        {
            using (DataContext db = new())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}