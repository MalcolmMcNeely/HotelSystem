using Guests.ViewModels;
using Guests.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HotelSystem.Infrastructure.Constants;
using HotelSystem.Data.DataTransferObjects;
using HotelSystem.Data.Repositories;
using Guests.Repositories;

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
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<GuestView>();
            containerRegistry.Register<IGuestViewViewModel, GuestViewViewModel>();
            containerRegistry.Register<IGuestRepository, GuestRepository>();
            containerRegistry.Register<IRepository<GuestDataTransferObject>, Repository<GuestDataTransferObject>>();

            _regionManager.RegisterViewWithRegion(RegionNames.GuestsRegion, typeof(GuestView));
        }
    }
}
