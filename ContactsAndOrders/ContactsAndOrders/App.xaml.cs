using ContactsAndOrders.ViewModels;
using ContactsAndOrders.Windows;
using System.Windows;

namespace ContactsAndOrders
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs startupEventArgs)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new ContactViewModel()
            };
            MainWindow.Show();
        }
    }
}