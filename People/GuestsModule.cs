﻿using HotelSystem.Infrastructure;
using Guests.ViewModels;
using Guests.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using HotelSystem.Business;

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
            StaticData.EnsureGuestsAreInDatabase();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<GuestView>();
            containerRegistry.Register<IGuestViewModel, GuestViewModel>();

            _regionManager.RegisterViewWithRegion(RegionNames.GuestsRegion, typeof(GuestView));
        }
    }
}
