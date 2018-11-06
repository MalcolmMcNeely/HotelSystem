using HotelSystem.Data.DataTransferObjects;
using HotelSystem.DataContexes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelSystem.Data.StaticData
{
    public class StaticGuestData
    {
        List<GuestDataTransferObject> _guests = new List<GuestDataTransferObject>();

        public StaticGuestData()
        {
            _guests.Add(new GuestDataTransferObject()
            {
                Name = "Test Person",
                Age = 37,
                AddressLineOne = "123 Fakestreet",
                AddressLineTwo = "Fake Estate",
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
