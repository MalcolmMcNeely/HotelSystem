using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations
{
   [Module(ModuleName = "ReservationsModule")]
   public class ReservationsModule : IModule
   {
      public void OnInitialized(IContainerProvider containerProvider)
      {
         throw new NotImplementedException();
      }

      public void RegisterTypes(IContainerRegistry containerRegistry)
      {
         throw new NotImplementedException();
      }
   }
}
