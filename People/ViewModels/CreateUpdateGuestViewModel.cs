using FluentValidation;
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
        private Guest _model;
        private GuestValidator _validator = new GuestValidator();

        public CreateUpdateGuestViewModel(Guest guest)
        {
            SetupCommands();

            _model = guest;
            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public void ShutDown()
        {
            if (_model != null)
            {
                _model.PropertyChanged -= OnModelPropertyChanged;
            }
        }

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
