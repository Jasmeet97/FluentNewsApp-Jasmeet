using System.Windows.Input;

namespace FluentNewsApp_Jasmeet.Commands
{
    public class RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null) : ICommand
    {
        private readonly Action<object> execute = execute;
        private readonly Func<object, bool> canExecute = canExecute;

        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || this.canExecute(parameter ?? false);
        }

        public void Execute(object? parameter)
        {
            this.execute(parameter ?? false);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
