using ContactBook_TwoWindows.Commands;
using System.Threading.Tasks;

namespace ContactsAndOrders.Commands.Async
{
    public abstract class BaseCommandAsync : BaseCommand
    {
        private bool _isExecute;
        public bool IsExecute
        {
            get => _isExecute;
            set
            {
                _isExecute = value;
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !_isExecute;
        }

        public override async void Execute(object? parameter)
        {
            IsExecute = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecute = false;
            }
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}