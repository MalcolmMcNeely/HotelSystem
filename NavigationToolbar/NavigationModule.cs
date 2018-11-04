using Navigation.Views;
using NavigationToolbar.Interfaces;
using NavigationToolbar.ViewModels;
using Prism.Ioc;
using Prism.Modularity;

namespace Navigation
{
   [Module(ModuleName = "NavigationModule")]
   public class NavigationModule : IModule
   {
      // this will give out infra.dryioccontainerextension
      public void OnInitialized(IContainerProvider containerProvider)
      {
         //throw new NotImplementedException();         
      }

      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         containerRegistry.Register<NavigationView>();
         containerRegistry.Register<INavigationViewModel, NavigationViewModel>();
      }
   }
}
