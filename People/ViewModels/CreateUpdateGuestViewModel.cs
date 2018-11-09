﻿using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Guests.Validators;
using HotelSystem.Business.DomainObjects;
using HotelSystem.Infrastructure.Common;
using Prism.Commands;
using System;
using System.ComponentModel;

namespace Guests.ViewModels
{
    public class CreateUpdateGuestViewModel : ValidatableBindableBase
    {
        private Guest _model = new Guest();
        private GuestValidator _validator = new GuestValidator();

        public CreateUpdateGuestViewModel()
        {
            SetupCommands();

            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public void ShutDown()
        {
            if (_model != null)
            {
                _model.PropertyChanged -= OnModelPropertyChanged;
            }
        }

        #region Bound Properties

        public string Name
        {
            get => _model.Name;
            set => _model.Name = value;
        }

        public int Age
        {
            get => _model.Age;
            set => _model.Age = value;
        }

        public string AddressLineOne
        {
            get => _model.AddressLineOne;
            set => _model.AddressLineOne = value;
        }

        public string AddressLineTwo
        {
            get => _model.AddressLineTwo;
            set => _model.AddressLineTwo = value;
        }

        public string City
        {
            get => _model.City;
            set => _model.City = value;
        }

        public string PostCode
        {
            get => _model.PostCode;
            set => _model.PostCode = value;
        }

        public string PhoneNumber
        {
            get => _model.PhoneNumber;
            set => _model.PhoneNumber = value;
        }

        public string CreditCardNumber
        {
            get => _model.CreditCardNumber;
            set => _model.CreditCardNumber = value;
        }

        public decimal AmountOwed
        {
            get => _model.AmountOwed;
            set => _model.AmountOwed = value;
        }

        public decimal AmountPaid
        {
            get => _model.AmountPaid;
            set => _model.AmountPaid = value;
        }

        #endregion

        #region Commands

        private void SetupCommands()
        {
            SaveCommand = new DelegateCommand(SaveCommandExecute).ObservesCanExecute(() => ValidationPassed);
        }

        public DelegateCommand SaveCommand { get; private set; }

        public void SaveCommandExecute()
        {
            ValidateModel();

            if (ValidationPassed)
            {
                _model.LastUpdated = DateTime.Now;
                _model.Save();
            }
        }

        #endregion

        #region Events

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ValidateModelProperty(e.PropertyName);
            RaisePropertyChanged(e.PropertyName);
        }

        #endregion

        public ValidationResult ValidateModel()
        {
            ClearAllErrors();

            var result = _validator.Validate(_model);

            AddErrors(result);

            return result;
        }

        public ValidationResult ValidateModelProperty(string propertyName)
        {
            ClearError(propertyName);

            var context = new ValidationContext<Guest>(
                _model, new PropertyChain(),
                new MemberNameValidatorSelector(new[] { propertyName }));
            var result = _validator.Validate(context);

            AddErrors(result);

            return result;
        }
    }
}
