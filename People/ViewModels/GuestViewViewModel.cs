using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Guests.Events;
using Guests.Models;
using Guests.Repositories;
using Guests.Validators;
using HotelSystem.Infrastructure.WPF;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Guests.ViewModels
{
    public class GuestViewViewModel : ValidatableBindableBase, IGuestViewViewModel
    {
        GuestViewViewModelValidator _validator = new GuestViewViewModelValidator();
        IGuestRepository _repository;
        IEventAggregator _eventAggregator;

        public GuestViewViewModel(IGuestRepository repository, 
                                  IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
        }

        public void Initialise()
        {
            AttachEvents();
            SetupCommands();

            PopulateAllGuestsFromDatabase();
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
                    var guestViewModel = value == null ? new GuestViewModel(new Guest()) : value;
                    _eventAggregator.GetEvent<GuestSelectedEvent>().Publish(guestViewModel);
                }
            }
        }

        private bool _isCreateUpdateGuestViewVisible;
        public bool IsCreateUpdateGuestViewVisible
        {
            get => _isCreateUpdateGuestViewVisible;
            set => SetProperty(ref _isCreateUpdateGuestViewVisible, value);
        }

        private string _filterString;
        public string FilterString
        {
            get => _filterString;
            set
            {
                if(SetProperty(ref _filterString, value))
                {
                    ValidateProperty(nameof(FilterString));

                    if(ValidationPassed)
                    {
                        FilterGuests();
                    }
                }
            }
        }

        private SmartCollection<GuestViewModel> _filteredGuests = 
            new SmartCollection<GuestViewModel>();
        public SmartCollection<GuestViewModel> FilteredGuests
        {
            get => _filteredGuests;
            set => SetProperty(ref _filteredGuests, value);
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

            var selectedGuest = SelectedGuest;

            if(selectedGuest != null)
            {
                Guests.Remove(selectedGuest);
                _repository.Remove(selectedGuest.Model.ToGuestDataTransferObject());
            }
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

        #region Validation

        public ValidationResult Validate()
        {
            ClearAllErrors();

            var result = _validator.Validate(this);

            AddErrors(result);

            return result;
        }

        public ValidationResult ValidateProperty(string propertyName)
        {
            ClearError(propertyName);

            var context = new ValidationContext<GuestViewViewModel>(this,
                new PropertyChain(), new MemberNameValidatorSelector(new[] { propertyName }));
            var result = _validator.Validate(context);

            AddErrors(result);

            return result;
        }

        #endregion

        private void PopulateAllGuestsFromDatabase()
        {
            Guests.Clear();

            var allGuestData = _repository.GetAll();
            var allGuestViewModel = new List<GuestViewModel>();

            foreach (var guest in allGuestData)
            {
                allGuestViewModel.Add(new GuestViewModel(new Guest(guest)));
            }

            Guests.Reset(allGuestViewModel);
            FilterGuests();
        }

        private void FilterGuests()
        {
            IEnumerable<GuestViewModel> filteredGuests = Guests;

            if(!String.IsNullOrEmpty(FilterString))
            {
                var filter = FilterString.ToUpper();

                filteredGuests = filteredGuests.Where(g => 
                    g.Name.ToUpper().Contains(filter) ||
                    g.AddressLineOne.ToUpper().Contains(filter) ||
                    g.AddressLineTwo.ToUpper().Contains(filter) ||
                    g.City.ToUpper().Contains(filter) ||
                    g.PostCode.ToUpper().Contains(filter)
                    );
            }

            FilteredGuests.Reset(filteredGuests);
        }
    }
}
