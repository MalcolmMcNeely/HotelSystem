using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelSystem.Data.DataModels;

namespace HotelSystem.DataContexes
{
    public class PersonContext : DbContext
    {
        public DbSet<DALPerson> People { get; set; }
    }
}
