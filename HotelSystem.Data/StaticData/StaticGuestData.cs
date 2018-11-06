using HotelSystem.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelSystem.Data.StaticData
{
    public class StaticGuestData
    {
        List<GuestData> _guests = new List<GuestData>();

        public StaticGuestData()
        {
            _guests.Add(new GuestData()
            {
                Name = "Test Person",
                Age = 37,
                AddressLineOne = "123 Fakestreet",
                AddressLineTwo = "Fake Estate",
                PostCode = "FAK3 C0DE",
                City = "Faketown",
                PhoneNumber = "01234567890",
                CreditCardNumber = "9876543210",
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });
        }

        public void EnsureGuestsAreInDatabase()
        {
            using (var db = new Context())
            {
                var query = db.Guests.FirstOrDefault();

                if(query == null)
                {
                    foreach(var guest in _guests)
                    {
                        db.Guests.Add(guest);
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}
