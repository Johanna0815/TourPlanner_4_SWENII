using System;
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


        // liefert uns ob das command bereit ausgeführt zuwerden wenn z.b false dann ist der button deactiviert
        public bool CanExecute(object? parameter)
        {
            return _canExecute.Invoke(parameter);
            //return true;
        }

        // wird ausgeführt sobald der button geklickt wurde,
        // dies ruft die Methode die hinter Relaycommand steht solange is nicht null 
        public void Execute(object? parameter) => this._execute?.Invoke(parameter);

    }

}
