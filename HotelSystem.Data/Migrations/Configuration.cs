namespace HotelSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelSystem.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelSystem.Data.Context context)
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
        }
    }
}
