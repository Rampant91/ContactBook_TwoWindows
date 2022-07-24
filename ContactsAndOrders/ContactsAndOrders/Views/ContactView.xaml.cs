using ContactsAndOrders.Models;
using ContactsAndOrders.ViewModels;
using ContactsAndOrders.Windows;
using System.Windows.Controls;

namespace ContactsAndOrders.Views
{
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrdersWindow ordersWindow = new OrdersWindow()
            {
                DataContext = new OrderViewModel((Contact)((ListBox)sender).SelectedItem)
            };
        }
    }
}