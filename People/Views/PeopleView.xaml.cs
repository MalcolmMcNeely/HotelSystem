using HotelSystem.Infrastructure.MVVM;
using People.ViewModels;
using System.Windows.Controls;

namespace People.Views
{
   /// <summary>
   /// Interaction logic for PeopleView.xaml
   /// </summary>
   public partial class PeopleView : UserControl, IView
   {
      public PeopleView(IPeopleViewModel viewModel)
      {
         InitializeComponent();

         ViewModel = viewModel;
      }

      public IViewModel ViewModel
      {
         get => (IViewModel) DataContext;
         set => DataContext = value;
      }
   }
}
