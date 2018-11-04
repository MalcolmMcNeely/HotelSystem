using HotelSystem.Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Reservations.Intefaces;
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
         containerRegistry.Register<ReservationsView>();
         containerRegistry.Register<IReservationsViewModel, ReservationsViewModel>();

         _regionManager.RegisterViewWithRegion(RegionNames.ReservationsRegion, typeof(ReservationsView));
      }
   }
}
