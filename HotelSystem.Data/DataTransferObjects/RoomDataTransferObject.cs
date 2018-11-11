using HotelSystem.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data.DataTransferObjects
{
    public class RoomDataTransferObject
    {
        public RoomDataTransferObject() { }

        public RoomDataTransferObject(RoomData data)
        {
            Id = data.Id;
            RoomNumber = data.RoomNumber;
            RoomTypeDataId = data.RoomTypeDataId;
            RoomTypeData = new RoomTypeDataTransferObject(data.RoomTypeData);
            Price = data.Price;
            OnPromotion = data.OnPromotion;
            OnPromotionPrice = data.OnPromotionPrice;
            IsOccupied = data.IsOccupied;
            BookedFrom = data.BookedFrom;
            BookedTo = data.BookedTo;
            Guests = data.Guests.Select(x => new GuestDataTransferObject(x)).ToList();
        }

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeDataId { get; set; }
        public RoomTypeDataTransferObject RoomTypeData { get; set; }
        public decimal Price { get; set; }
        public bool OnPromotion { get; set; }
        public decimal OnPromotionPrice { get; set; }
        public bool IsOccupied { get; set; }
        public DateTime? BookedFrom { get; set; }
        public DateTime? BookedTo { get; set; }
        public ICollection<GuestDataTransferObject> Guests { get; set; }

        public RoomData ToRoomData()
        {
            return new RoomData
            {
                Id = Id,
                RoomNumber = RoomNumber,
                RoomTypeDataId = RoomTypeDataId,
                RoomTypeData = RoomTypeData.ToRoomTypeData(),
                Price = Price,
                OnPromotion = OnPromotion,
                OnPromotionPrice = OnPromotionPrice,
                IsOccupied = IsOccupied,
                BookedFrom = BookedFrom,
                BookedTo = BookedTo,
                Guests = Guests.Select(x => x.ToGuestData()).ToList()
            };
        }
    }
}
