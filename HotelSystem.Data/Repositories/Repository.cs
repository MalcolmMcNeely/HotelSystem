using HotelSystem.Data.Contexts;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace HotelSystem.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public T Get(int id)
        {
            using (var context = new HotelContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public List<T> GetAll()
        {
            using (var context = new HotelContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Add(params T[] entities)
        {
            using (var context = new HotelContext())
            {
                context.Set<T>().AddRange(entities);
                context.SaveChanges();
            }
        }

        public void AddOrUpdate(T entity)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    context.Set<T>().AddOrUpdate(entity);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    // TODO: refactor this, use it in the other operations
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    throw;
                }
            }
        }

        public void Remove(params T[] entities)
        {
            using (var context = new HotelContext())
            {
                foreach (var entity in entities)
                {
                    // Entities need to be attached if they were
                    // not loaded by the same context instance
                    context.Set<T>().Attach(entity);
                    context.Set<T>().Remove(entity);
                }

                context.SaveChanges();
            }
        }
    }
}
