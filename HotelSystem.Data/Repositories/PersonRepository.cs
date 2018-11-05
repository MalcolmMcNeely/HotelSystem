using HotelSystem.Data.DataModels;
using HotelSystem.DataContexes;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
   public class PersonRepository
   {
      public void AddOrUpdate(DALPerson person)
      {
         using (var db = new PersonContext())
         {
            var existingPerson = db.People.Find(person.Id);

            if(existingPerson != null)
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

      public ObservableCollection<DALPerson> GetAll()
      {
         using (var db = new PersonContext())
         {
            return db.People.Local;
         }
      }

      public DALPerson GetById(int id)
      {
         using (var db = new PersonContext())
         {
            return db.People.Find(id);
         }
      }
   }
}
