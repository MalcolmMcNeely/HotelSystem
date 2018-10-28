using CommonServiceLocator;
using DryIoc;
using HotelSystem.Ioc;
using Prism;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelSystem
{
   public class PrismApplication : PrismApplicationBase
   {
      /// <summary>
      /// Create <see cref="Rules" /> to alter behavior of <see cref="IContainer" />
      /// </summary>
      /// <returns>An instance of <see cref="Rules" /></returns>
      protected virtual Rules CreateContainerRules() => Rules.Default.WithAutoConcreteTypeResolution()
                                                                     .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
                                                                     .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace);

      protected override IContainerExtension CreateContainerExtension()
      {
         return new DryIocContainerExtension(new Container(CreateContainerRules()));
      }

      protected override Window CreateShell()
      {
         return new Shell();
         //return Container.Resolve<Shell>();
      }

      protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
      {
         base.RegisterRequiredTypes(containerRegistry);

         containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
         containerRegistry.RegisterSingleton<IServiceLocator, DryIocServiceLocatorAdapter>();
      }

      protected override void RegisterTypes(IContainerRegistry containerRegistry)
      {
         base.RegisterFrameworkExceptionTypes();

         ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ContainerException));
      }
   }
}
