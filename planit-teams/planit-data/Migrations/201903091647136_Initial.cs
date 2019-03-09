namespace planit_data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        ExchangeName = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.BoardId);
            
            CreateTable(
                "dbo.CardLists",
                c => new
                    {
                        ListId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Color = c.String(unicode: false),
                        Board_BoardId = c.Int(),
                    })
                .PrimaryKey(t => t.ListId)
                .ForeignKey("dbo.Boards", t => t.Board_BoardId)
                .Index(t => t.Board_BoardId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 0),
                        DueDate = c.DateTime(nullable: false, precision: 0),
                        User_UserId = c.Int(),
                        List_ListId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.CardLists", t => t.List_ListId)
                .Index(t => t.User_UserId)
                .Index(t => t.List_ListId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, unicode: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 0),
                        Card_CardId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Cards", t => t.Card_CardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Card_CardId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        ExchangeName = c.String(nullable: false, unicode: false),
                        IdentificationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentificationUser_Id)
                .Index(t => t.IdentificationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        IsRead = c.Boolean(nullable: false),
                        NotificationType = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        BelongsToUserId = c.Int(nullable: false),
                        Card_CardId = c.Int(),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Users", t => t.BelongsToUserId, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_CardId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId, cascadeDelete: true)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.BelongsToUserId)
                .Index(t => t.Card_CardId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionId = c.Int(nullable: false, identity: true),
                        IsAdmin = c.Boolean(nullable: false),
                        Board_BoardId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Boards", t => t.Board_BoardId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Board_BoardId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CardObserverUser",
                c => new
                    {
                        CardRefId = c.Int(nullable: false),
                        UserRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardRefId, t.UserRefId })
                .ForeignKey("dbo.Cards", t => t.CardRefId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .Index(t => t.CardRefId)
                .Index(t => t.UserRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CardObserverUser", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.CardObserverUser", "CardRefId", "dbo.Cards");
            DropForeignKey("dbo.Cards", "List_ListId", "dbo.CardLists");
            DropForeignKey("dbo.Comments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Permissions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Permissions", "Board_BoardId", "dbo.Boards");
            DropForeignKey("dbo.Notifications", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "Card_CardId", "dbo.Cards");
            DropForeignKey("dbo.Notifications", "BelongsToUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "IdentificationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cards", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Card_CardId", "dbo.Cards");
            DropForeignKey("dbo.CardLists", "Board_BoardId", "dbo.Boards");
            DropIndex("dbo.CardObserverUser", new[] { "UserRefId" });
            DropIndex("dbo.CardObserverUser", new[] { "CardRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Permissions", new[] { "User_UserId" });
            DropIndex("dbo.Permissions", new[] { "Board_BoardId" });
            DropIndex("dbo.Notifications", new[] { "Card_CardId" });
            DropIndex("dbo.Notifications", new[] { "BelongsToUserId" });
            DropIndex("dbo.Notifications", new[] { "CreatedByUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "IdentificationUser_Id" });
            DropIndex("dbo.Comments", new[] { "User_UserId" });
            DropIndex("dbo.Comments", new[] { "Card_CardId" });
            DropIndex("dbo.Cards", new[] { "List_ListId" });
            DropIndex("dbo.Cards", new[] { "User_UserId" });
            DropIndex("dbo.CardLists", new[] { "Board_BoardId" });
            DropTable("dbo.CardObserverUser");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Notifications");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Cards");
            DropTable("dbo.CardLists");
            DropTable("dbo.Boards");
        }
    }
}
