using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ConwaysGameOfLife.ViewModels.Commands
{
    public class PlayGameCommand : ICommand
    {
        private Action _excute;

        public PlayGameCommand(Action execute)
        {
            _excute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (PlayingFieldViewModel.PopulationIsRunning)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            _excute.Invoke();
        }
    }
}
