using HotelSystem.Data.Data;
using System;

namespace HotelSystem.Data.DataTransferObjects
{
    public class GuestDataTransferObject
    {
        public GuestDataTransferObject() { }

        public GuestDataTransferObject(GuestData data)
        {
            Id = data.Id;
            Name = data.Name;
            Age = data.Age;
            AddressLineOne = data.AddressLineOne;
            AddressLineTwo = data.AddressLineTwo;
            City = data.City;
            PhoneNumber = data.PhoneNumber;
            CreditCardNumber = data.CreditCardNumber;
            AmountOwed = data.AmountOwed;
            AmountPaid = data.AmountPaid;
            DateCreated = data.DateCreated;
            LastUpdated = data.LastUpdated;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal AmountOwed { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public GuestData ToGuestData()
        {
            return new GuestData
            {
                Id = Id,
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
        }
    }
}
