using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationToolbar
{
   [Module(ModuleName = "NavigationToolbarModule")]
   public class NavigationToolbarModule : IModule
   {
      public void OnInitialized(IContainerProvider containerProvider)
      {
         //throw new NotImplementedException();
      }

      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         //throw new NotImplementedException();
      }
   }
}
