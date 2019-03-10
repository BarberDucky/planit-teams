namespace planit_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardNotifications",
                c => new
                    {
                        BoardNotificationId = c.Int(nullable: false, identity: true),
                        IsRead = c.Boolean(nullable: false),
                        BoardNotificationType = c.Int(nullable: false),
                        Board_BoardId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BoardNotificationId)
                .ForeignKey("dbo.Boards", t => t.Board_BoardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Board_BoardId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoardNotifications", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.BoardNotifications", "Board_BoardId", "dbo.Boards");
            DropIndex("dbo.BoardNotifications", new[] { "User_UserId" });
            DropIndex("dbo.BoardNotifications", new[] { "Board_BoardId" });
            DropTable("dbo.BoardNotifications");
        }
    }
}
