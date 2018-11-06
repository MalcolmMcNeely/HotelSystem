using System.Data.Entity;
using HotelSystem.Data.Data;

namespace HotelSystem.Data
{
    public class Context : DbContext
    {
        public DbSet<GuestData> Guests { get; set; }
    }
}
