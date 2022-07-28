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
    public class DeleteOrderCommand : BaseCommandAsync
    {
        private readonly OrderViewModel _orderViewModel;

        public DeleteOrderCommand(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _orderViewModel.PropertyChanged += OrderViewModelPropertyChanged;
        }

        private void OrderViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.SelectedOrder))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _orderViewModel.SelectedOrder != null;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var order = _orderViewModel.SelectedOrder;
            _orderViewModel.Orders?.Remove(order!);
            await Task.Run(() => DeleteOrder(order!));
        }

        private void DeleteOrder(Order order)
        {
            using (DataContext db = new())
            {
                var orderDb = db.Orders?.FirstOrDefault(x => x.OrderId == order.OrderId);
                if (orderDb != null)
                {
                    db.Orders?.Remove(orderDb);
                    Thread.Sleep(5000);
                    db.SaveChanges();
                }
            }
        }
    }
}