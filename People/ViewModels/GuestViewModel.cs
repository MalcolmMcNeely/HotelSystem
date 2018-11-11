using HotelSystem.Infrastructure.WPF;
using Guests.Validators;
using System.ComponentModel;
using Guests.Models;
using System;
using HotelSystem.Infrastructure.WPF.Attributes;

namespace Guests.ViewModels
{
    public class GuestViewModel : BindableBase
    {
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

        [DisplayName("Address Line One")]
        public string AddressLineOne
        {
            get => _model.AddressLineOne;
            set => _model.AddressLineOne = value;
        }

        [DisplayName("Address Line Two")]
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

        [DisplayName("Post Code")]
        public string PostCode
        {
            get => _model.PostCode;
            set => _model.PostCode = value;
        }

        [DisplayName("Phone Number")]
        public string PhoneNumber
        {
            get => _model.PhoneNumber;
            set => _model.PhoneNumber = value;
        }

        [DisplayName("Credit Card Number")]
        public string CreditCardNumber
        {
            get => _model.CreditCardNumber;
            set => _model.CreditCardNumber = value;
        }

        [DisplayName("Amount Owed")]
        public decimal AmountOwed
        {
            get => _model.AmountOwed;
            set => _model.AmountOwed = value;
        }

        [DisplayName("Amount Paid")]
        public decimal AmountPaid
        {
            get => _model.AmountPaid;
            set => _model.AmountPaid = value;
        }

        [DisplayName("Date Created")]
        public DateTime DateCreated
        {
            get => _model.DateCreated;
            set => _model.DateCreated = value;
        }

        [DisplayName("Last Updated")]
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
