using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ConwaysGameOfLife.ViewModels.Commands
{
    public class RestartGameCommand : ICommand
    {
        private Action _execute;
        
        public RestartGameCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
