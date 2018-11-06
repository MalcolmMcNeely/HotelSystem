using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using HotelSystem.Data.DataTransferObjects;

namespace HotelSystem.DataContexes
{
    public class GuestContext : DbContext
    {
        public DbSet<GuestDataTransferObject> People { get; set; }
    }
}
