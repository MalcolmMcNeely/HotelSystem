using HotelSystem.Data.DataModels;
using System.Collections.ObjectModel;

namespace HotelSystem.Data.Repositories
{
   public interface IPersonRepository
   {
      void AddOrUpdate(DALPerson person);
      ObservableCollection<DALPerson> GetAll();
      DALPerson GetById(int id);
   }
}
