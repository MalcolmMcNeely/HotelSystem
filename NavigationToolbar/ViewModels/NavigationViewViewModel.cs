using HotelSystem.Infrastructure.Constants;
using Prism.Commands;
using Prism.Regions;

namespace NavigationToolbar.ViewModels
{
    public class NavigationViewViewModel : INavigationViewViewModel
    {
        IRegionManager _regionManager;

        public NavigationViewViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialise()
        {
            SetupCommands();
        }

        public void ShutDown()
        {
            
        }

        #region Commands

        public void SetupCommands()
        {
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecute);
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        private void NavigateCommandExecute(string navigationPath)
        {
            _regionManager.RequestNavigate(RegionNames.ShellContentRegion, navigationPath);
        }

        #endregion
    }
}
