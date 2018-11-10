using Guests.Events;
using Guests.Models;
using Guests.Repositories;
using HotelSystem.Infrastructure.WPF;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;

namespace Guests.ViewModels
{
    public class GuestViewViewModel : BindableBase, IGuestViewViewModel
    {
        IGuestRepository _repository;
        IEventAggregator _eventAggregator;

        public GuestViewViewModel(IGuestRepository repository, 
                                  IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;

            PopulateAllGuestsFromDatabase();
            AttachEvents();
        }

        public void Initialise()
        {
            SetupCommands();
        }

        public void ShutDown()
        {
            foreach (var guest in Guests)
            {
                guest.ShutDown();
            }

            EditingGuest?.ShutDown();
        }

        #region Bound Properties

        private readonly SmartCollection<GuestViewModel> _guests = new SmartCollection<GuestViewModel>();
        public SmartCollection<GuestViewModel> Guests
        {
            get => _guests;
        }

        private GuestViewModel _selectedGuest;
        public GuestViewModel SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if (SetProperty(ref _selectedGuest, value))
                {
                    EditingGuest = value;
                }
            }
        }

        private GuestViewModel _editingGuest;
        public GuestViewModel EditingGuest
        {
            get => _editingGuest;
            set
            {
                if (SetProperty(ref _editingGuest, value))
                {
                    _eventAggregator.GetEvent<GuestSelectedEvent>().Publish(value);
                }
            }
        }

        private bool _isCreateUpdateGuestViewVisible;
        public bool IsCreateUpdateGuestViewVisible
        {
            get => _isCreateUpdateGuestViewVisible;
            set => SetProperty(ref _isCreateUpdateGuestViewVisible, value);
        }

        #endregion

        #region Properties

        public bool IsGuestSelected
        {
            get => EditingGuest != null;
        }

        #endregion

        #region Commands

        private void SetupCommands()
        {
            CreateGuestCommand = new DelegateCommand(CreateGuestCommandExecute);
            UpdateGuestCommand = new DelegateCommand(UpdateGuestCommandExecute)
                .ObservesCanExecute(() => IsGuestSelected)
                .ObservesProperty(() => EditingGuest);
            DeleteGuestCommand = new DelegateCommand(DeleteGuestCommandExecute)
                .ObservesCanExecute(() => IsGuestSelected)
                .ObservesProperty(() => EditingGuest);
        }

        public DelegateCommand CreateGuestCommand { get; private set; }

        public void CreateGuestCommandExecute()
        {
            EditingGuest = new GuestViewModel(new Guest());
            IsCreateUpdateGuestViewVisible = true;
        }

        public DelegateCommand UpdateGuestCommand { get; private set; }

        public void UpdateGuestCommandExecute()
        {
            IsCreateUpdateGuestViewVisible = true;
        }

        public DelegateCommand DeleteGuestCommand { get; private set; }

        public void DeleteGuestCommandExecute()
        {
            EditingGuest = new GuestViewModel(new Guest());
            
        }

        #endregion

        #region Events

        public void AttachEvents()
        {
            _eventAggregator.GetEvent<GuestUpdatedEvent>().Subscribe(GuestViewModelUpdated);
        }

        private void GuestViewModelUpdated(GuestViewModel guestViewModel)
        {
            var matchedGuest = (from g in Guests
                                where g.Model.Id == guestViewModel.Model.Id
                                select g).FirstOrDefault();

            if(matchedGuest != null)
            {
                matchedGuest.Update(guestViewModel);
            }
            else
            {
                PopulateAllGuestsFromDatabase();
            }
        }

        #endregion

        private void PopulateAllGuestsFromDatabase()
        {
            Guests.Clear();

            var allGuestData = _repository.GetAll();

            foreach (var guest in allGuestData)
            {
                Guests.Add(new GuestViewModel(new Guest(guest)));
            }
        }
    }
}
