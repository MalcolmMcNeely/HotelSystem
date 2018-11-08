using System.Data.Entity;
using HotelSystem.Data.Data;

namespace HotelSystem.Data
{
    public class Context : DbContext
    {
        public DbSet<GuestData> Guests { get; set; }

        public DbSet<RoomData> Rooms { get; set; }

        public DbSet<RoomTypeData> RoomTypes { get; set; }
    }
}
