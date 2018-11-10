using HotelSystem.Data.Data;
using HotelSystem.Data.DataTransferObjects;
using HotelSystem.Data.Repositories;
using System.Collections.Generic;

namespace Guests.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        IRepository<GuestData> _repository;

        public GuestRepository(IRepository<GuestData> repository)
        {
            _repository = repository;
        }

        public void AddOrUpdate(GuestDataTransferObject person)
        {
            _repository.AddOrUpdate(person.ToGuestData());
        }

        public IList<GuestDataTransferObject> GetAll()
        {
            var allData = _repository.GetAll();

            var dto = new List<GuestDataTransferObject>();

            foreach(var data in allData)
            {
                dto.Add(new GuestDataTransferObject(data));
            }

            return dto;
        }

        public GuestDataTransferObject Get(int id)
        {
            var data = _repository.Get(id);

            if(data != null)
            {
                return new GuestDataTransferObject(data);
            }

            return null;
        }
    }
}
