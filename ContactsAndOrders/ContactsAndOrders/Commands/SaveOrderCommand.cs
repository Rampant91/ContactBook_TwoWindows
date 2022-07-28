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
    public class SaveOrderCommand : BaseCommandAsync
    {
        private readonly OrderViewModel _orderViewModel;

        public SaveOrderCommand(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            _orderViewModel.PropertyChanged += OrderViewModelPropertyChanged;
        }

        private void OrderViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OrderViewModel.SelectedOrder)
                || e.PropertyName == nameof(OrderViewModel.Name)
                || e.PropertyName == nameof(OrderViewModel.Date)
                || e.PropertyName == nameof(OrderViewModel.Price)
                || e.PropertyName == nameof(OrderViewModel.Amount)
                || e.PropertyName == nameof(OrderViewModel.Discription)
                || e.PropertyName == nameof(OrderViewModel.Editable))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _orderViewModel.SelectedOrder != null && _orderViewModel.Editable == true;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Order order = _orderViewModel.SelectedOrder!;
            _orderViewModel.Editable = false;
            await Task.Run(() => SaveOrder(order));
        }

        private void SaveOrder(Order order)
        {
            using (DataContext db = new())
            {
                var orderDb = db.Orders?.FirstOrDefault(x => x.OrderId == order.OrderId);
                if (orderDb != null)
                {
                    orderDb.Name = order.Name;
                    orderDb.Date = order.Date;
                    orderDb.Price = order.Price;
                    orderDb.Amount = order.Amount;
                    orderDb.Discription = order.Discription;
                    Thread.Sleep(5000);
                    db.SaveChanges();
                }
            }
        }
    }
}