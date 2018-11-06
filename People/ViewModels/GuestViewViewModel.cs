using HotelSystem.Business.DataManagers;
using HotelSystem.Infrastructure.Common;
using Prism.Commands;
using System;

namespace Guests.ViewModels
{
    public class GuestViewViewModel : BindableBase, IGuestViewViewModel
    {
        GuestManager _guestManager = new GuestManager();

        public void Initialise()
        {
            SetupCommands();

            var allGuests = _guestManager.GetAllGuests();

            foreach(var guest in allGuests)
            {
                Guests.Add(new GuestViewModel(guest));
            }
        }

        public void ShutDown()
        {
            foreach(var guest in Guests)
            {
                guest.ShutDown();
            }
        }

        #region Properties

        private SmartCollection<GuestViewModel> _guests = new SmartCollection<GuestViewModel>();
        public SmartCollection<GuestViewModel> Guests
        {
            get => _guests;
        }

        private GuestViewModel _selectedGuest;
        public GuestViewModel SelectedGuest
        {
            get => _selectedGuest;
            set => SetProperty(ref _selectedGuest, value);
        }

        private bool _isCreateUpdateGuestViewVisible;
        public bool IsCreateUpdateGuestViewVisible
        {
            get => _isCreateUpdateGuestViewVisible;
            set => SetProperty(ref _isCreateUpdateGuestViewVisible, value);
        }

        #endregion

        #region Commands

        private void SetupCommands()
        {
            ShowCreateUpdateGuestViewCommand =
                new DelegateCommand(ShowCreateUpdateGuestViewCommandExecute);
        }

        public DelegateCommand ShowCreateUpdateGuestViewCommand { get; private set; }

        public void ShowCreateUpdateGuestViewCommandExecute()
        {
            IsCreateUpdateGuestViewVisible = true;
        }

        #endregion
    }
}
