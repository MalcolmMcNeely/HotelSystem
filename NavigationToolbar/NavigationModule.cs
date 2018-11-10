using HotelSystem.Infrastructure.Constants;
using Navigation.Views;
using NavigationToolbar.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Navigation
{
    [Module(ModuleName = "NavigationModule")]
    public class NavigationModule : IModule
    {
        IRegionManager _regionManager;

        public NavigationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // this will give out infra.dryioccontainerextension
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //throw new NotImplementedException();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<NavigationView>();
            containerRegistry.Register<INavigationViewModel, NavigationViewModel>();

            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }
    }
}
