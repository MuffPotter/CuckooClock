using System;
using System.Windows.Input;

namespace Sigeko.AppFramework.Commands
{
    public class DelegateCommand : ICommand
    {
        private Predicate<object> _canExecuteHandler;
        private Action<object> _executeHandler;
        private bool _canExecuteCache;

        public DelegateCommand(Action<object> executeHandler)
        {
            _executeHandler = executeHandler;
        }

        public DelegateCommand(Predicate<object> canExecuteHandler, Action<object> executeHandler)
        {
            _canExecuteHandler = canExecuteHandler;
            _executeHandler = executeHandler;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null)
            {
                return true;
            }
            var result = _canExecuteHandler(parameter);
            if (result != _canExecuteCache)
            {
                _canExecuteCache = result;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
            return result;
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }
    }
}
