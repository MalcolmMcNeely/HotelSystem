using HotelSystem.Data.DataTransferObjects;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
    public interface IGuestRepository
    {
        void AddOrUpdate(GuestDataTransferObject person);
        ObservableCollection<GuestDataTransferObject> GetAll();
        GuestDataTransferObject GetById(int id);
    }
}
