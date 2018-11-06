using HotelSystem.Data.DataTransferObjects;
using HotelSystem.DataContexes;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
    public class GuestRepository
    {
        public void AddOrUpdate(GuestDataTransferObject person)
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

        public ObservableCollection<GuestDataTransferObject> GetAll()
        {
            using (var db = new Context())
            {
                return db.Guests.Local;
            }
        }

        public GuestDataTransferObject GetById(int id)
        {
            using (var db = new Context())
            {
                return db.Guests.Find(id);
            }
        }
    }
}
