using ContactsAndOrders.Models;
using System.Windows;

namespace ContactsAndOrders.Windows
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Show();
        }
    }
}