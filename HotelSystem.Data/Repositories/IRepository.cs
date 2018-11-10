using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IList<T> GetAll();
        void Add(params T[] entities);
        void AddOrUpdate(T entity);
        void Remove(params T[] entities);
    }
}
