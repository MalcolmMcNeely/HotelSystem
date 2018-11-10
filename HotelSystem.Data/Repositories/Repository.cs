using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public T Get(int id)
        {
            using (var context = new Context())
            {
                return context.Set<T>().Find(id);
            }
        }

        public IList<T> GetAll()
        {
            using (var context = new Context())
            {
                return context.Set<T>().Local;
            }
        }

        public void Add(params T[] entities)
        {
            using (var context = new Context())
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
            }
        }

        public void AddOrUpdate(T entity)
        {
            using (var context = new Context())
            {
                context.Set<T>().AddOrUpdate(entity);
                context.SaveChanges();
            }
        }

        public void Remove(params T[] entities)
        {
            using (var context = new Context())
            {
                context.Set<T>().RemoveRange(entities);
                context.SaveChanges();
            }
        }
    }
}
