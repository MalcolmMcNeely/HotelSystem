using HotelSystem.Infrastructure.MVVM;
using Reservations.Intefaces;
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
         set => DataContext = value;
      }
   }
}
