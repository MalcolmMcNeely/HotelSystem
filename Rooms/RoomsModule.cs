using HotelSystem.Infrastructure.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Rooms.ViewModels;
using Rooms.Views;

namespace Rooms
{
    [Module(ModuleName = "RoomsModule")]
    public class RoomsModule : IModule
    {
        IRegionManager _regionManager;

        public RoomsModule(IRegionManager regionManager)
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
            containerRegistry.Register<RoomsView>();

            // Interfaces
            containerRegistry.Register<IRoomsViewViewModel, RoomsViewViewModel>();

            // Navigation
            containerRegistry.RegisterForNavigation(typeof(RoomsView), RegionNames.RoomsView);
        }
    }
}
