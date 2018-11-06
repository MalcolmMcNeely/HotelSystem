using HotelSystem.Business.DomainObjects;
using HotelSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Business.DataManagers
{
    public class GuestManager
    {
        IGuestRepository _repository;

        public GuestManager()
        {
            _repository = new GuestRepository();
        }

        public List<Guest> GetAllGuests()
        {
            var guestData = _repository.GetAll();
            var guests = new List<Guest>();

            foreach(var g in guestData)
            {
                guests.Add(new Guest(g));
            }

            return guests;
        }


    }
}
