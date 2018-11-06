using HotelSystem.Data.DataModels;
using HotelSystem.Data.Repositories;
using HotelSystem.DataContexes;
using HotelSystem.Infrastructure.Common;
using System;
using System.ComponentModel;

namespace HotelSystem.Business
{
    public partial class Person : BindableBase
    {
        PersonRepository _repository = new PersonRepository();

        public Person()
        {
        }

        public Person(Person other)
        {
            _name = other._name;
            _age = other._age;
            _addressLineOne = other._addressLineOne;
            _addressLineTwo = other.AddressLineTwo;
            _city = other.City;
            _postCode = other.PostCode;
            _phoneNumber = other._phoneNumber;
            _creditCardNumber = other._creditCardNumber;
            _amountOwed = other._amountOwed;
            _amountPaid = other.AmountPaid;
        }

        #region Properties

        public int Key { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _age;
        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _addressLineOne;
        public string AddressLineOne
        {
            get => _addressLineOne;
            set => SetProperty(ref _addressLineOne, value);
        }

        private string _addressLineTwo;
        public string AddressLineTwo
        {
            get => _addressLineTwo;
            set => SetProperty(ref _addressLineTwo, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _postCode;
        public string PostCode
        {
            get => _postCode;
            set => SetProperty(ref _postCode, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _creditCardNumber;
        public string CreditCardNumber
        {
            get => _creditCardNumber;
            set => SetProperty(ref _creditCardNumber, value);
        }

        private decimal _amountOwed;
        public decimal AmountOwed
        {
            get => _amountOwed;
            set => SetProperty(ref _amountOwed, value);
        }

        private decimal _amountPaid;
        public decimal AmountPaid
        {
            get => _amountPaid;
            set => SetProperty(ref _amountPaid, value);
        }

        private DateTime _dateCreated = DateTime.Now;
        public DateTime DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get => _lastUpdated;
            set => SetProperty(ref _lastUpdated, value);
        }

        #endregion

        public DALPerson ToDALPerson()
        {
            var dalPerson = new DALPerson()
            {
                Name = Name,
                Age = Age,
                AddressLineOne = AddressLineOne,
                AddressLineTwo = AddressLineTwo,
                City = City,
                PhoneNumber = PhoneNumber,
                CreditCardNumber = CreditCardNumber,
                AmountOwed = AmountOwed,
                AmountPaid = AmountPaid,
                DateCreated = DateCreated,
                LastUpdated = LastUpdated
            };

            return dalPerson;
        }

        public void Save()
        {
            var person = ToDALPerson();
            _repository.AddOrUpdate(ToDALPerson());
        }
    }
}
