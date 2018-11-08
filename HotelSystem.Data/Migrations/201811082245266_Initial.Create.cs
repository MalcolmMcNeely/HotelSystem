namespace HotelSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GuestDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        AddressLineOne = c.String(nullable: false),
                        AddressLineTwo = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        CreditCardNumber = c.String(),
                        AmountOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(nullable: false),
                        RoomTypeDataId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OnPromotion = c.Boolean(nullable: false),
                        OnPromotionPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsOccupied = c.Boolean(nullable: false),
                        BookedFrom = c.DateTime(nullable: false),
                        BookTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomTypeDatas", t => t.RoomTypeDataId, cascadeDelete: true)
                .Index(t => t.RoomTypeDataId);
            
            CreateTable(
                "dbo.RoomTypeDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NumberOfBeds = c.Int(nullable: false),
                        HasBalcony = c.Boolean(nullable: false),
                        IsDisabilityFriendly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomDatas", "RoomTypeDataId", "dbo.RoomTypeDatas");
            DropIndex("dbo.RoomDatas", new[] { "RoomTypeDataId" });
            DropTable("dbo.RoomTypeDatas");
            DropTable("dbo.RoomDatas");
            DropTable("dbo.GuestDatas");
        }
    }
}
