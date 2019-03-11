namespace planit_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardNotifications1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BoardNotifications", "BoardNotificationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoardNotifications", "BoardNotificationType", c => c.Int(nullable: false));
        }
    }
}
