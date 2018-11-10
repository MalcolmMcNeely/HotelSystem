using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Guests.Models;
using Guests.Repositories;
using Guests.Validators;
using HotelSystem.Infrastructure.Common;
using Prism.Commands;
using System;
using System.ComponentModel;

namespace Guests.ViewModels
{
    public class CreateUpdateGuestViewModel : ValidatableBindableBase, ICreateUpdateGuestViewModel
    {
        private GuestValidator _validator = new GuestValidator();
        IGuestRepository _repository;

        public CreateUpdateGuestViewModel(IGuestRepository repository)
        {
            _repository = repository;
            SetupCommands();
            AttachEvents();
        }

        public void Initialise()
        {
            
        }

        public void ShutDown()
        {
            DetachEvents();
        }

        #region Bound Properties

        public string Name
        {
            get => _editedModel.Name;
            set => _editedModel.Name = value;
        }

        public int Age
        {
            get => _editedModel.Age;
            set => _editedModel.Age = value;
        }

        public string AddressLineOne
        {
            get => _editedModel.AddressLineOne;
            set => _editedModel.AddressLineOne = value;
        }

        public string AddressLineTwo
        {
            get => _editedModel.AddressLineTwo;
            set => _editedModel.AddressLineTwo = value;
        }

        public string City
        {
            get => _editedModel.City;
            set => _editedModel.City = value;
        }

        public string PostCode
        {
            get => _editedModel.PostCode;
            set => _editedModel.PostCode = value;
        }

        public string PhoneNumber
        {
            get => _editedModel.PhoneNumber;
            set => _editedModel.PhoneNumber = value;
        }

        public string CreditCardNumber
        {
            get => _editedModel.CreditCardNumber;
            set => _editedModel.CreditCardNumber = value;
        }

        public decimal AmountOwed
        {
            get => _editedModel.AmountOwed;
            set => _editedModel.AmountOwed = value;
        }

        public decimal AmountPaid
        {
            get => _editedModel.AmountPaid;
            set => _editedModel.AmountPaid = value;
        }

        #endregion

        #region Properties

        private GuestViewModel _model = new GuestViewModel(new Guest());
        public GuestViewModel Model
        {
            get => _model;
            set
            {
                if(SetProperty(ref _model, value))
                {
                    EditedModel = new GuestViewModel(_model);                    
                }
            }
        }

        private GuestViewModel _editedModel = new GuestViewModel(new Guest());
        public GuestViewModel EditedModel
        {
            get => _editedModel;
            set
            {
                var oldModel = _model;

                if (SetProperty(ref _editedModel, value))
                {
                    if (oldModel != null)
                    {
                        oldModel.PropertyChanged -= OnModelPropertyChanged;
                    }

                    AttachEvents();
                    RaisePropertyChanged(string.Empty);
                }
            }
        }

        #endregion

        #region Commands

        private void SetupCommands()
        {
            SaveCommand = new DelegateCommand(SaveCommandExecute).ObservesCanExecute(() => ValidationPassed);
            UndoCommand = new DelegateCommand(UndoCommandExecute);
        }

        public DelegateCommand SaveCommand { get; private set; }

        public void SaveCommandExecute()
        {
            ValidateModel();

            if (ValidationPassed)
            {
                _editedModel.LastUpdated = DateTime.Now;

                _model = _editedModel;

                _repository.AddOrUpdate(_model.Model.ToGuestDataTransferObject());
            }
        }

        public DelegateCommand UndoCommand { get; private set; }

        public void UndoCommandExecute()
        {
            EditedModel = Model;
            ClearAllErrors();
        }

        #endregion

        #region Events

        private void AttachEvents()
        {
            if(_model != null)
            {
                _editedModel.PropertyChanged += OnModelPropertyChanged;
            }
        }

        private void DetachEvents()
        {
            if (_model != null)
            {
                _editedModel.PropertyChanged -= OnModelPropertyChanged;
            }
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ValidateModelProperty(e.PropertyName);
            RaisePropertyChanged(e.PropertyName);
        }

        #endregion

        public ValidationResult ValidateModel()
        {
            ClearAllErrors();

            var result = _validator.Validate(EditedModel.Model);

            AddErrors(result);

            return result;
        }

        public ValidationResult ValidateModelProperty(string propertyName)
        {
            ClearError(propertyName);

            var context = new ValidationContext<Guest>(
                EditedModel.Model, new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
            var result = _validator.Validate(context);

            AddErrors(result);

            return result;
        }
    }
}
