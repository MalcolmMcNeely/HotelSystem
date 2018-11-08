namespace HotelSystem.Data.Migrations
{
    using HotelSystem.Data.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            context.Guests.AddOrUpdate(new GuestData()
            {
                Name = "Test Person",
                Age = 37,
                AddressLineOne = "123 Fakestreet",
                AddressLineTwo = "Fake Estate",
                PostCode = "FAK3 C0DE",
                City = "Faketown",
                PhoneNumber = "01234567890",
                CreditCardNumber = "9876543210",
                DateCreated = DateTime.Now,
                LastUpdated = DateTime.Now
            });
        }
    }
}
