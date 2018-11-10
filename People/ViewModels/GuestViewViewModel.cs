using Guests.Models;
using Guests.Repositories;
using HotelSystem.Infrastructure.Common;
using Prism.Commands;
using System;

namespace Guests.ViewModels
{
    public class GuestViewViewModel : BindableBase, IGuestViewViewModel
    {
        IGuestRepository _repository;

        public GuestViewViewModel(IGuestRepository repository)
        {
            _repository = repository;

            var allGuestData = _repository.GetAll();

            foreach(var guest in allGuestData)
            {
                Guests.Add(new GuestViewModel(new Guest(guest)));
            }
        }

        public void Initialise()
        {
            SetupCommands();
        }

        public void ShutDown()
        {
            foreach(var guest in Guests)
            {
                guest.ShutDown();
            }

            EditingGuest?.ShutDown();
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

        private GuestViewModel _editingGuest;
        public GuestViewModel EditingGuest
        {
            get => _editingGuest;
            set => SetProperty(ref _editingGuest, value);
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
            CreateGuestCommand =
                new DelegateCommand(CreateGuestCommandExecute);
        }

        public DelegateCommand CreateGuestCommand { get; private set; }

        public void CreateGuestCommandExecute()
        {
            EditingGuest = new GuestViewModel(new Guest());
            IsCreateUpdateGuestViewVisible = true;
        }

        #endregion
    }
}
