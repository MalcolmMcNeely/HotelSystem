using HotelSystem.Data.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotelSystem.Data.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        public void AddOrUpdate(GuestData person)
        {
            using (var db = new Context())
            {
                var existingPerson = db.Guests.Find(person.Id);

                if (existingPerson != null)
                {
                    existingPerson.Update(person);
                    db.SaveChanges();
                }
                else
                {
                    db.Guests.Add(person);
                    db.SaveChanges();
                }
            }
        }

        public List<GuestData> GetAll()
        {
            using (var db = new Context())
            {
                return db.Guests.ToList();
            }
        }

        public GuestData GetById(int id)
        {
            using (var db = new Context())
            {
                return db.Guests.Find(id);
            }
        }
    }
}
