using System.Data.Entity;
using HotelSystem.Data.DataTransferObjects;

namespace HotelSystem.DataContexes
{
    public class Context : DbContext
    {
        public DbSet<GuestDataTransferObject> Guests { get; set; }
    }
}
