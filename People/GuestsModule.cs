using Guests.ViewModels;
using Guests.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HotelSystem.Infrastructure.Constants;
using HotelSystem.Data.DataTransferObjects;
using HotelSystem.Data.Repositories;
using Guests.Repositories;
using HotelSystem.Data.Data;
using Guests.Resources;

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
            // Views
            containerRegistry.Register<GuestView>();
            containerRegistry.Register<CreateUpdateGuestView>();

            // Interfaces
            containerRegistry.Register<IGuestViewViewModel, GuestViewViewModel>();
            containerRegistry.Register<ICreateUpdateGuestViewModel, CreateUpdateGuestViewModel>();

            // Repositories
            containerRegistry.Register<IGuestRepository, GuestRepository>();
            containerRegistry.Register<IRepository<GuestData>, Repository<GuestData>>();

            // Navigation
            containerRegistry.RegisterForNavigation(typeof(GuestView), RegionNames.GuestsView);

            // Local Regions
            _regionManager.RegisterViewWithRegion(Strings.CreateUpdateGuestRegion, typeof(CreateUpdateGuestView));
        }
    }
}
