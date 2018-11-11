using HotelSystem.Infrastructure.WPF.MVVM;
using NavigationToolbar.ViewModels;
using System.Windows.Controls;

namespace Navigation.Views
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class NavigationView : UserControl, IView
    {
        public NavigationView(INavigationViewViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get => (IViewModel)DataContext;
            set => DataContext = value;
        }
    }
}
