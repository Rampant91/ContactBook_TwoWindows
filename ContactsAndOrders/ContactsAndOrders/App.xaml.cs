using ContactBook_TwoWindows.ViewModels;
using ContactBook_TwoWindows.Windows;
using System.Windows;

namespace ContactBook_TwoWindows
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