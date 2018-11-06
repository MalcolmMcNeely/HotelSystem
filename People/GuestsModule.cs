using HotelSystem.Infrastructure;
using Guests.ViewModels;
using Guests.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Guests
{
    [Module(ModuleName = "GuestsModule")]
    public class GuestsModule : IModule
    {
        IRegionManager _regionManager;

        public GuestsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //throw new System.NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<GuestView>();
            containerRegistry.Register<IGuestViewModel, GuestViewModel>();

            _regionManager.RegisterViewWithRegion(RegionNames.GuestsRegion, typeof(GuestView));
        }
    }
}
