using System;
using System.Linq;
using System.Linq.Expressions;

namespace HotelSystem.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void AddOrUpdate(T entity);
        void Remove(T entity);
        void Save();
    }
}
