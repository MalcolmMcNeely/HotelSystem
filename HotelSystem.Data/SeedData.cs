using HotelSystem.Data.Data;
using System;
using System.Collections.Generic;

namespace HotelSystem.Data
{
    public class SeedData
    {
        public List<GuestData> GetGuestData()
        {
            var guestData = new List<GuestData>();

            guestData.Add(new GuestData()
            {
                Name = "Clive",
                Age = 64,
                AddressLineOne = "Flat 1",
                AddressLineTwo = "99  Warren St",
                PostCode = "SO30 1ZA",
                City = "Birmingham",
                PhoneNumber = "03069 990103",
                CreditCardNumber = "4539148587987719",
                AmountOwed = 0M,
                AmountPaid = 0M,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            guestData.Add(new GuestData()
            {
                Name = "Jimmy",
                Age = 15,
                AddressLineOne = "Flat 1",
                AddressLineTwo = "99  Warren St",
                PostCode = "SO30 1ZA",
                City = "Birmingham",
                PhoneNumber = "03069 990783",
                CreditCardNumber = "343816485977867",
                AmountOwed = 0M,
                AmountPaid = 0M,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            guestData.Add(new GuestData()
            {
                Name = "Andrea",
                Age = 28,
                AddressLineOne = "123 Fakestreet",
                AddressLineTwo = "Fake Estate",
                PostCode = "FAK3 C0DE",
                City = "Faketown",
                PhoneNumber = "03069 990458",
                CreditCardNumber = "6011076971077072",
                AmountOwed = 0M,
                AmountPaid = 0M,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            guestData.Add(new GuestData()
            {
                Name = "Mordred",
                Age = 31,
                AddressLineOne = "1st Barracks",
                AddressLineTwo = "",
                PostCode = "CE01 2UK",
                City = "Camelot",
                PhoneNumber = "03069 990787",
                CreditCardNumber = "5355044801071976",
                AmountOwed = 0M,
                AmountPaid = 0M,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            guestData.Add(new GuestData()
            {
                Name = "Arthur",
                Age = 52,
                AddressLineOne = "Throne Room",
                AddressLineTwo = "",
                PostCode = "CE01 2UK",
                City = "Camelot",
                PhoneNumber = "03069 990295",
                CreditCardNumber = "4716011593316627",
                AmountOwed = 0M,
                AmountPaid = 0M,
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });

            return guestData;
        }

    }
}
