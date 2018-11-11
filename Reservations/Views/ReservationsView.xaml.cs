using HotelSystem.Infrastructure.WPF.MVVM;
using Reservations.ViewModels;
using System.Windows.Controls;

namespace Reservations.Views
{
    /// <summary>
    /// Interaction logic for ReservationsView.xaml
    /// </summary>
    public partial class ReservationsView : UserControl, IView
    {
        public ReservationsView(IReservationsViewModel viewModel)
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
