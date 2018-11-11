using HotelSystem.Data.DataTransferObjects;
using HotelSystem.Infrastructure.WPF;
using Rooms.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooms.Models
{
    public class Room : BindableBase
    {
        public Room() { }

        public Room(Room other)
        {
            UpdateFromRoom(other);
        }

        public Room(RoomDataTransferObject data)
        {
            _id = data.Id;
            _roomNumber = data.RoomNumber;
            _roomType = new RoomType(data.RoomTypeData);
            _price = data.Price;
            _onPromotion = data.OnPromotion;
            _onPromotionPrice = data.OnPromotionPrice;
            _isOccupied = data.IsOccupied;
            _bookedFrom = data.BookedFrom;
            _bookedTo = data.BookedTo;
            _guestIds = (from g in data.Guests
                        select g.Id).ToList();
        }

        #region Properties

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _roomNumber;
        public string RoomNumber
        {
            get => _roomNumber;
            set => SetProperty(ref _roomNumber, value);
        }

        private RoomType _roomType;
        public RoomType RoomType
        {
            get => _roomType;
            set => SetProperty(ref _roomType, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private bool _onPromotion;
        public bool OnPromotion
        {
            get => _onPromotion;
            set => SetProperty(ref _onPromotion, value);
        }

        private decimal _onPromotionPrice;
        public decimal OnPromotionPrice
        {
            get => _onPromotionPrice;
            set => SetProperty(ref _onPromotionPrice, value);
        }

        private bool _isOccupied;
        public bool IsOccupied
        {
            get => _isOccupied;
            set => SetProperty(ref _isOccupied, value);
        }

        private DateTime? _bookedFrom;
        public DateTime? BookedFrom
        {
            get => _bookedFrom;
            set => SetProperty(ref _bookedFrom, value);
        }

        private DateTime? _bookedTo;
        public DateTime? BookedTo
        {
            get => _bookedTo;
            set => SetProperty(ref _bookedTo, value);
        }

        private List<int> _guestIds;
        public List<int> GuestIds
        {
            get => _guestIds;
            set => SetProperty(ref _guestIds, value);
        }

        #endregion

        public void Update(Room other)
        {
            UpdateFromRoom(other);
        }

        public RoomDataTransferObject ToRoomDataTransferObject()
        {
            return new RoomDataTransferObject
            {
                Id = Id,
                RoomNumber = RoomNumber,
                RoomTypeDataId = RoomType.Id,
                RoomTypeData = RoomType.ToRoomTypeDataTransferObject(),
                Price = Price,
                OnPromotion = OnPromotion,
                OnPromotionPrice = OnPromotionPrice,
                IsOccupied = IsOccupied,
                BookedFrom = BookedFrom,
                BookedTo = BookedTo,
            };
        }

        private void UpdateFromRoom(Room other)
        {
            _id = other.Id;
            _roomNumber = other.RoomNumber;
            _roomType = new RoomType(other.RoomType);
            _price = other.Price;
            _onPromotion = other.OnPromotion;
            _onPromotionPrice = other.OnPromotionPrice;
            _isOccupied = other.IsOccupied;
            _bookedFrom = other.BookedFrom;
            _bookedTo = other.BookedTo;
            _guestIds = new List<int>(other.GuestIds);
        }
    }
}
