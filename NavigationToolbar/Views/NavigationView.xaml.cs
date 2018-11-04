using HotelSystem.Infrastructure.MVVM;
using NavigationToolbar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Navigation.Views
{
   /// <summary>
   /// Interaction logic for Toolbar.xaml
   /// </summary>
   public partial class NavigationView : UserControl, IView
   {
      public NavigationView(INavigationViewModel viewModel)
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
