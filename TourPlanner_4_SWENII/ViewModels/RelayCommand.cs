using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner_4_SWENII.ViewModels
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(null, execute)
        {

        }

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);


        // liefert uns ob das command bereit ausgeführt zuwerden wenn z.b false dann ist das button deactiviert
        public bool CanExecute(object? parameter)
        {
            _canExecute.Invoke(parameter);
            return true;

        }



        // die wird ausgeführt sobald das button geclickt wurde dies ruft die Methode die hinter Realycommadn so lange is nicht null 
        public void Execute(object? parameter) => this._execute?.Invoke(parameter);

    }

}
