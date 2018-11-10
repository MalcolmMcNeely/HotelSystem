using HotelSystem.Infrastructure.Common;
using Guests.Validators;
using System.ComponentModel;
using Guests.Models;
using System;
using HotelSystem.Infrastructure.Attributes;

namespace Guests.ViewModels
{
    public class GuestViewModel : BindableBase
    {
        private GuestValidator _validator = new GuestValidator();

        public GuestViewModel(Guest guest)
        {
            Model = guest;
            AttachEvents();
        }

        public GuestViewModel(GuestViewModel other)
        {
            Model = new Guest(other.Model);
            AttachEvents();
        }

        public void Update(GuestViewModel other)
        {
            Model.Update(other.Model);
        }

        public void ShutDown()
        {
            DetachEvents();
        }

        #region Model Accessors

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

        [DoNotAutoGenerate]
        public DateTime LastUpdated
        {
            get => _model.LastUpdated;
            set => _model.LastUpdated = value;
        }

        #endregion

        #region Properties

        private Guest _model;
        [DoNotAutoGenerate]
        public Guest Model
        {
            get => _model;
            private set
            {
                Guest oldGuest = _model;

                if (SetProperty(ref _model, value))
                {
                    if(oldGuest != null)
                    {
                        oldGuest.PropertyChanged -= OnModelPropertyChanged;
                    }

                    AttachEvents();
                    RaisePropertyChanged(string.Empty);
                }
            }
        }

        #endregion

        #region Events

        private void AttachEvents()
        {
            if (_model != null)
            {
                _model.PropertyChanged += OnModelPropertyChanged;
            }
        }

        private void DetachEvents()
        {
            if (_model != null)
            {
                _model.PropertyChanged -= OnModelPropertyChanged;
            }
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        #endregion
    }
}
