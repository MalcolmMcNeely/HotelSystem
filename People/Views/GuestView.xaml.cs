using HotelSystem.Infrastructure.WPF.MVVM;
using Guests.ViewModels;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Guests.Views
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : UserControl, IView
    {
        public GuestView(IGuestViewViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get => (IViewModel)DataContext;
            set
            {
                if(DataContext != null)
                {
                    ((IViewModel)DataContext).ShutDown();
                }

                DataContext = value;

                ((IViewModel)DataContext).Initialise();
            }
        }

        private void ColumnHeaderContextMenuHideItemClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                var itemParent = item.Parent as ContextMenu;

                if (itemParent != null)
                {
                    var target = itemParent.PlacementTarget as DataGridColumnHeader;

                    if (target != null)
                    {
                        target.Column.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }
}
