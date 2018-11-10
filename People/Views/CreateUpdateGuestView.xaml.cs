using Guests.Models;
using Guests.ViewModels;
using HotelSystem.Infrastructure.MVVM;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Guests.Views
{
    /// <summary>
    /// Interaction logic for CreateUpdateGuest.xaml
    /// </summary>
    public partial class CreateUpdateGuestView : UserControl, IView
    {
        public CreateUpdateGuestView(ICreateUpdateGuestViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public GuestViewModel Guest
        {
            get => (GuestViewModel)GetValue(GuestProperty);
            set => SetValue(GuestProperty, value);
        }

        public static DependencyProperty GuestProperty =
            DependencyProperty.Register("Guest", typeof(GuestViewModel), 
                typeof(CreateUpdateGuestView), new FrameworkPropertyMetadata(OnGuestPropertyChanged));

        private static void OnGuestPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as CreateUpdateGuestView;

            if (view != null)
            {
                var viewModel = view.ViewModel as ICreateUpdateGuestViewModel;

                if(viewModel != null)
                {
                    viewModel.Model = (GuestViewModel)e.NewValue;
                }
            }
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
