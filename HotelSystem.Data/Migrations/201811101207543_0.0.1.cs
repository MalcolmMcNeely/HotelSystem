namespace HotelSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuestDatas", "RoomData_Id", c => c.Int());
            AddColumn("dbo.RoomDatas", "BookedTo", c => c.DateTime(nullable: false));
            CreateIndex("dbo.GuestDatas", "RoomData_Id");
            AddForeignKey("dbo.GuestDatas", "RoomData_Id", "dbo.RoomDatas", "Id");
            DropColumn("dbo.RoomDatas", "BookTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomDatas", "BookTo", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.GuestDatas", "RoomData_Id", "dbo.RoomDatas");
            DropIndex("dbo.GuestDatas", new[] { "RoomData_Id" });
            DropColumn("dbo.RoomDatas", "BookedTo");
            DropColumn("dbo.GuestDatas", "RoomData_Id");
        }
    }
}
