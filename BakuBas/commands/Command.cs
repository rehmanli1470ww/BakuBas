using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BakuBas.commands
{
    public class Command : ICommand
    {
        public Command(Action<object>? action, Predicate<object>? predicat)
        {
            this.action = action;
            this.predicat = predicat;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Action<object>? action { get; set; }
        public Predicate<object>? predicat { get; set; }

        public bool CanExecute(object? parameter)
        {
            return predicat?.Invoke(parameter!) ?? false;
        }

        public void Execute(object? parameter)
        {
            action?.Invoke(parameter!);
        }
    }
}
