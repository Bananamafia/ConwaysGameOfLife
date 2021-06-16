using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Conways.DesktopApp.ViewModels.Commands
{
    class PopulateCommand : ICommand
    {
        public PopulateCommand(Action action)
        {
            _action = action;
        }

        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}
