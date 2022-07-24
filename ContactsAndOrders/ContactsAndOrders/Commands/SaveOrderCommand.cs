﻿using ContactsAndOrders.DbRealization;
using ContactsAndOrders.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace ContactsAndOrders.Commands
{
    public class SaveOrderCommand : BaseCommand
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

        public override void Execute(object? parameter)
        {
            var order = _orderViewModel.SelectedOrder;
            using (DataContext db = new DataContext())
            {
                var orderDb = db.Orders?.FirstOrDefault(x => x.OrderId == order.OrderId);
                if (orderDb != null)
                {
                    orderDb.Name = order.Name;
                    orderDb.Date = order.Date;
                    orderDb.Price = order.Price;
                    orderDb.Amount = order.Amount;
                    orderDb.Discription = order.Discription;
                    db.SaveChanges();
                }
                _orderViewModel.Editable = false;
            }
        }
    }
}