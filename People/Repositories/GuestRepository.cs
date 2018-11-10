using HotelSystem.Data.DataTransferObjects;
using HotelSystem.Data.Repositories;
using System.Collections.Generic;

namespace Guests.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        IRepository<GuestDataTransferObject> _repository;

        public GuestRepository(IRepository<GuestDataTransferObject> repository)
        {
            _repository = repository;
        }

        public void AddOrUpdate(GuestDataTransferObject person)
        {
            _repository.AddOrUpdate(person);
        }

        public IList<GuestDataTransferObject> GetAll()
        {
            return _repository.GetAll();
        }

        public GuestDataTransferObject Get(int id)
        {
            return _repository.Get(id);
        }
    }
}
