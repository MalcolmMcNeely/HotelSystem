using System;
using System.Windows;
using System.Windows.Input;

namespace HotelSystem.Infrastructure.WPF.Styles.MainWindow
{
    public class WindowMaximizeCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = parameter as Window;

            if (window != null)
            {
                window.WindowState = window.WindowState == WindowState.Maximized ?
                   WindowState.Normal : WindowState.Maximized;
            }
        }
    }
}
