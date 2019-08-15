using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Religious_Composition_Program_Ver01
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Action _executeWithoutParam;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
            _execute = execute;
        }

        public RelayCommand(Action executeWithoutParam)
        {
            _executeWithoutParam = executeWithoutParam;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute();
            else
                return true;
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
                _execute.Invoke(parameter);
            else
                _executeWithoutParam.Invoke();
        }
    }
}
