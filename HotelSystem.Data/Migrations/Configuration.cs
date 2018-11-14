namespace HotelSystem.Data.Migrations
{
    using HotelSystem.Data.Contexts;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var seedData = new SeedData();
            var guests = seedData.GetGuestData();

            foreach (var guest in guests)
            {
                context.Guests.AddOrUpdate(guest);
            }

            var roomTypes = seedData.GetRoomTypeData();

            foreach (var roomType in roomTypes)
            {
                context.RoomTypes.AddOrUpdate(roomType);
            }

            var rooms = seedData.GetRoomData(roomTypes);

            foreach (var room in rooms)
            {
                context.Rooms.AddOrUpdate(room);
            }
        }
    }
}
