using System.Data.Entity;
using HotelSystem.Data.Data;

namespace HotelSystem.Data.Contexts
{
    public class HotelContext : DbContext
    {
        public DbSet<GuestData> Guests { get; set; }

        public DbSet<RoomData> Rooms { get; set; }

        public DbSet<RoomTypeData> RoomTypes { get; set; }
    }
}
