using HotelSystem.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data.DataTransferObjects
{
    public class RoomTypeDataTransferObject
    {
        public RoomTypeDataTransferObject() { }

        public RoomTypeDataTransferObject(RoomTypeData data)
        {
            Id = data.Id;
            Name = data.Name;
            NumberOfBeds = data.NumberOfBeds;
            HasBalcony = data.HasBalcony;
            IsDisabilityFriendly = data.IsDisabilityFriendly;
            RoomData = data.RoomData.Select(x => new RoomDataTransferObject(x)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public bool HasBalcony { get; set; }
        public bool IsDisabilityFriendly { get; set; }
        public ICollection<RoomDataTransferObject> RoomData { get; set; }

        public RoomTypeData ToRoomTypeData()
        {
            return new RoomTypeData
            {
                Id = Id,
                Name = Name,
                NumberOfBeds = NumberOfBeds,
                HasBalcony = HasBalcony,
                IsDisabilityFriendly = IsDisabilityFriendly,
                RoomData = RoomData.Select(x => x.ToRoomData()).ToList()
            };
        }
    }
}
