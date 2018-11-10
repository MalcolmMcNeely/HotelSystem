using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public List<T> GetAll()
        {
            using (var context = new Context())
            {
                return context.Set<T>().ToList();
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
                try
                {
                    context.Set<T>().AddOrUpdate(entity);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    if (!System.Diagnostics.Debugger.IsAttached)
                        System.Diagnostics.Debugger.Launch();

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
            using (var context = new Context())
            {
                context.Set<T>().RemoveRange(entities);
                context.SaveChanges();
            }
        }
    }
}
