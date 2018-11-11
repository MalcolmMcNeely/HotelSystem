using HotelSystem.Infrastructure.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Reservations.ViewModels;
using Reservations.Views;

namespace Reservations
{
    [Module(ModuleName = "ReservationsModule")]
    public class ReservationsModule : IModule
    {
        IRegionManager _regionManager;

        public ReservationsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Views
            containerRegistry.Register<ReservationsView>();

            // Interfaces
            containerRegistry.Register<IReservationsViewModel, ReservationsViewModel>();

            // Navigation
            containerRegistry.RegisterForNavigation(typeof(ReservationsView), RegionNames.ReservationsView);
        }
    }
}
