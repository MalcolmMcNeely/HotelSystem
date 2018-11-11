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
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
                Id = 4,
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
                Id = 5,
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

        public List<RoomTypeData> GetRoomTypeData()
        {
            var roomTypeData = new List<RoomTypeData>();

            roomTypeData.Add(new RoomTypeData
            {
                Id = 1,
                Name = "Single Room",
                NumberOfBeds = 1,
                HasBalcony = false,
                IsDisabilityFriendly = true
            });

            roomTypeData.Add(new RoomTypeData
            {
                Id = 2,
                Name = "Single Room (Executive)",
                NumberOfBeds = 1,
                HasBalcony = true,
                IsDisabilityFriendly = true
            });

            roomTypeData.Add(new RoomTypeData
            {
                Id = 3,
                Name = "Double Room",
                NumberOfBeds = 2,
                HasBalcony = false,
                IsDisabilityFriendly = true
            });

            roomTypeData.Add(new RoomTypeData
            {
                Id = 4,
                Name = "Double Room (Executive)",
                NumberOfBeds = 2,
                HasBalcony = true,
                IsDisabilityFriendly = true
            });

            roomTypeData.Add(new RoomTypeData
            {
                Id = 5,
                Name = "Triple Room",
                NumberOfBeds = 3,
                HasBalcony = false,
                IsDisabilityFriendly = false
            });

            roomTypeData.Add(new RoomTypeData
            {
                Id = 6,
                Name = "Triple Room (Executive)",
                NumberOfBeds = 3,
                HasBalcony = true,
                IsDisabilityFriendly = false
            });

            return roomTypeData;
        }

        public List<RoomData> GetRoomData(List<RoomTypeData> roomTypeData)
        {
            var roomData = new List<RoomData>();
            var roomsPerRoomType = 10;

            for (int i = 0; i < roomTypeData.Count; i++)
            {
                var price = 40M * (i + 1);
                var demandMultiplier = 5M;
                var demandLevel = 0;

                for (int j = 0; j < roomsPerRoomType; j++)
                {
                    if(j % 2 == 0)
                    {
                        demandLevel++;
                    }

                    roomData.Add(new RoomData
                    {
                        Id = i + j + 1,
                        RoomNumber = String.Format("{0}0{1}", i, j),
                        RoomTypeDataId = roomTypeData[i].Id,
                        RoomTypeData = roomTypeData[i],
                        Price = price + (demandLevel * demandMultiplier),
                        OnPromotion = false,
                        OnPromotionPrice = price * 0.80M,
                        IsOccupied = false
                    });
                }
            }

            return roomData;
        }
    }
}
