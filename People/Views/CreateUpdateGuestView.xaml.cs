using Guests.ViewModels;
using HotelSystem.Infrastructure.MVVM;
using System.Windows.Controls;

namespace Guests.Views
{
    /// <summary>
    /// Interaction logic for CreateUpdateGuest.xaml
    /// </summary>
    public partial class CreateUpdateGuestView : UserControl
    {
        CreateUpdateGuestViewModel _viewModel = new CreateUpdateGuestViewModel();

        public CreateUpdateGuestView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
