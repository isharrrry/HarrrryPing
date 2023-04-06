using System;
using System.Windows.Input;

namespace 云智慧
{
    public class DelegateCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;
        public DelegateCommand(Action executeMethod)
        {
            _execute = executeMethod;
        }
        public DelegateCommand(Action executeMethod, Func<bool> canExecute)
            : this(executeMethod)
        {
            this._canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }


            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public void Execute(object parameter)
        {
            _execute();
        }
    }
}