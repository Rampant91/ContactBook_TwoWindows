using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using ContactBook_TwoWindows.Windows;
using System.Windows.Controls;

namespace ContactBook_TwoWindows.Views
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
                DataContext = new OrderViewModel(((Contact)((ListBox)sender).SelectedItem).ContactId)
            };
            ordersWindow.ShowDialog();
        }
    }
}