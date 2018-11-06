using HotelSystem.Data.DataTransferObjects;
using HotelSystem.DataContexes;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
    public class GuestRepository
    {
        public void AddOrUpdate(GuestDataTransferObject person)
        {
            using (var db = new GuestContext())
            {
                var existingPerson = db.People.Find(person.Id);

                if (existingPerson != null)
                {
                    existingPerson.Update(person);
                    db.SaveChanges();
                }
                else
                {
                    db.People.Add(person);
                    db.SaveChanges();
                }
            }
        }

        public ObservableCollection<GuestDataTransferObject> GetAll()
        {
            using (var db = new GuestContext())
            {
                return db.People.Local;
            }
        }

        public GuestDataTransferObject GetById(int id)
        {
            using (var db = new GuestContext())
            {
                return db.People.Find(id);
            }
        }
    }
}
