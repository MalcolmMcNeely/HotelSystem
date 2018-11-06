using HotelSystem.Data.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
    public interface IGuestRepository
    {
        void AddOrUpdate(GuestData person);
        List<GuestData> GetAll();
        GuestData GetById(int id);
    }
}
