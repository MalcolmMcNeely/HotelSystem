using HotelSystem.Infrastructure;
using People.ViewModels;
using People.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace People
{
   [Module(ModuleName = "PeopleModule")]
   public class PeopleModule : IModule
   {
      IRegionManager _regionManager;

      public PeopleModule(IRegionManager regionManager)
      {
         _regionManager = regionManager;
      }

      public void OnInitialized(IContainerProvider containerProvider)
      {
         //throw new System.NotImplementedException();
      }

      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         containerRegistry.Register<PeopleView>();
         containerRegistry.Register<IPeopleViewModel, PeopleViewModel>();

         _regionManager.RegisterViewWithRegion(RegionNames.PeopleRegion, typeof(PeopleView));
      }
   }
}
