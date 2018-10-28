using CommonServiceLocator;
using DryIoc;
using HotelSystem.Infrastructure;
using HotelSystem.Ioc;
using NavigationToolbar;
using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
         return Container.Resolve<Shell>();
      }

      protected override void InitializeShell(Window shell)
      {
         base.InitializeShell(shell);

         App.Current.MainWindow = shell;
         App.Current.MainWindow.Show();
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

      protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
      {
         base.ConfigureRegionAdapterMappings(regionAdapterMappings);

         regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
      }

      protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
      {
         base.ConfigureModuleCatalog(moduleCatalog);

         var navigationToolbarType = typeof(NavigationToolbarModule);
         moduleCatalog.AddModule(new ModuleInfo()
         {
            ModuleName = navigationToolbarType.Name,
            ModuleType = navigationToolbarType.AssemblyQualifiedName,
            InitializationMode = InitializationMode.WhenAvailable
         });
      }
   }
}
