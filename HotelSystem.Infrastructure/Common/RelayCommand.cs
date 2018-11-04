using System;
using System.Windows.Input;

namespace HotelSystem.Infrastructure.Common
{
   public class RelayCommand : ICommand
   {
      readonly Action _execute;
      readonly Func<bool> _canExecute;

      public RelayCommand(Action execute)
          : this(execute, null)
      {
      }

      public RelayCommand(Action execute, Func<bool> canExecute)
      {
         if (execute == null)
            throw new ArgumentNullException("execute");

         _execute = execute;
         _canExecute = canExecute;
      }

      public void Execute(object parameter)
      {
         _execute();
      }

      public bool CanExecute(object parameter)
      {
         if (_canExecute == null)
            return true;

         return _canExecute.Invoke();
      }

      public event EventHandler CanExecuteChanged;

      public void RaiseCanExecuteChanged()
      {
         if (CanExecuteChanged != null)
            CanExecuteChanged.Invoke(this, EventArgs.Empty);
      }
   }
}
