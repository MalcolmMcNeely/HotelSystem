using HotelSystem.Infrastructure.MVVM;
using Guests.ViewModels;
using System.Windows.Controls;

namespace Guests.Views
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : UserControl, IView
    {
        public GuestView(IGuestViewModel viewModel)
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
