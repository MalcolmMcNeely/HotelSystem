using HotelSystem.Data.Data;
using HotelSystem.Data.DataTransferObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Guests.Repositories
{
    public interface IGuestRepository
    {
        void AddOrUpdate(GuestDataTransferObject person);
        List<GuestDataTransferObject> GetAll();
        GuestDataTransferObject Get(int id);
    }
}
