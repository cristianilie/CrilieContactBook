using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrilieContactBook.ViewModels.Commands
{
    public class ViewSwitchCommand : ICommand
    {
        private Action _exec;

        public ViewSwitchCommand(Action exec)
        {
            if (exec == null)
                throw new NullReferenceException("Exec is null!");

            _exec = exec;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _exec.Invoke();
        }
    }
}
