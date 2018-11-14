using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace HotelSystem.Data.Repositories
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T> 
        where T : class 
        where C : DbContext, new()
    {
        public C Context { get; } = new C();

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void AddOrUpdate(T entity)
        {
            Context.Set<T>().AddOrUpdate(entity);
        }

        public void Remove(T entity)
        {
            var entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                // Entities need to be attached if they were
                // not loaded by the same context instance
                Context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Deleted;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
