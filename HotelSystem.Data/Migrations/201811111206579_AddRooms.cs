namespace HotelSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRooms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GuestDatas", "RoomId", c => c.Int());
            AlterColumn("dbo.RoomDatas", "BookedFrom", c => c.DateTime());
            AlterColumn("dbo.RoomDatas", "BookedTo", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomDatas", "BookedTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RoomDatas", "BookedFrom", c => c.DateTime(nullable: false));
            DropColumn("dbo.GuestDatas", "RoomId");
        }
    }
}
