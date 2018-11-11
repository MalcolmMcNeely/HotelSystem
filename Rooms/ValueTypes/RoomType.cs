using HotelSystem.Data.DataTransferObjects;

namespace Rooms.ValueTypes
{
    public class RoomType
    {
        public RoomType(RoomTypeDataTransferObject data)
        {
            Id = data.Id;
            Name = data.Name;
            NumberOfBeds = data.NumberOfBeds;
            HasBalcony = data.HasBalcony;
            IsDisabilityFriendly = data.IsDisabilityFriendly;
        }

        public RoomType(RoomType other)
        {
            Id = other.Id;
            Name = other.Name;
            NumberOfBeds = other.NumberOfBeds;
            HasBalcony = other.HasBalcony;
            IsDisabilityFriendly = other.IsDisabilityFriendly;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int NumberOfBeds { get; private set; }
        public bool HasBalcony { get; private set; }
        public bool IsDisabilityFriendly { get; private set; }

        public RoomTypeDataTransferObject ToRoomTypeDataTransferObject()
        {
            return new RoomTypeDataTransferObject
            {
                Id = Id,
                Name = Name,
                NumberOfBeds = NumberOfBeds,
                HasBalcony = HasBalcony,
                IsDisabilityFriendly = IsDisabilityFriendly
            };
        }
    }
}
