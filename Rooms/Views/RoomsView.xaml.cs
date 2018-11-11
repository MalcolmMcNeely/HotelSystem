using HotelSystem.Infrastructure.WPF.MVVM;
using Rooms.ViewModels;
using System.Windows.Controls;

namespace Rooms.Views
{
    /// <summary>
    /// Interaction logic for RoomsView.xaml
    /// </summary>
    public partial class RoomsView : UserControl, IView
    {
        public RoomsView(IRoomsViewViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get => (IViewModel)DataContext;
            set
            {
                if (DataContext != null)
                {
                    ((IViewModel)DataContext).ShutDown();
                }

                DataContext = value;

                ((IViewModel)DataContext).Initialise();
            }
        }
    }
}
