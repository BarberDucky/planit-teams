using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using planit_data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            :base("name=ApplicationContext")
        {
            Database.SetInitializer<ApplicationContext>(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        public System.Data.Entity.DbSet<User> UserList { get; set; }
        public System.Data.Entity.DbSet<Board> Boards { get; set; }
        public System.Data.Entity.DbSet<Permission> Permissions { get; set; }
        public System.Data.Entity.DbSet<Card> Cards { get; set; }
        public System.Data.Entity.DbSet<CardList> CardLists { get; set; }
        public System.Data.Entity.DbSet<Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<Notification> Notifications { get; set; }
       // public System.Data.Entity.DbSet<BoardNotification> BoardNotifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardList>()
                .HasOptional<Board>(b => b.Board)
                .WithMany(b => b.CardLists)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Card>()
                .HasMany<User>(u => u.ObserverUsers)
                .WithMany(c => c.WatchedCards)
                .Map(cs =>
                {
                    cs.MapLeftKey("CardRefId");
                    cs.MapRightKey("UserRefId");
                    cs.ToTable("CardObserverUser");
                });

            modelBuilder.Entity<Card>()
               .HasOptional<CardList>(b => b.List)
               .WithMany(b => b.Cards)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Card>()
               .HasOptional<User>(b => b.User)
               .WithMany(b => b.Cards)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
              .HasOptional<Card>(b => b.Card)
              .WithMany(b => b.Comments)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
               .HasOptional<User>(b => b.User)
               .WithMany()
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Notification>()
                .HasRequired<User>(n => n.BelongsToUser)
                .WithMany(u => u.Notifications)
                .HasForeignKey<int>(u => u.BelongsToUserId);
                

            modelBuilder.Entity<Notification>()
                .HasRequired<User>(n => n.CreatedByUser)
                .WithMany()
                .HasForeignKey<int>(u => u.CreatedByUserId);

            modelBuilder.Entity<Notification>()
               .HasOptional<Card>(n => n.Card)
               .WithMany()
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<BoardNotification>()
                .HasOptional<User>(u => u.User)
                .WithMany(u => u.BoardNotification)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BoardNotification>()
               .HasOptional<Board>(u => u.Board)
               .WithMany(u => u.BoardNotification)
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Permission>()
              .HasOptional<User>(u => u.User)
              .WithMany(u => u.Permissions)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<Permission>()
               .HasOptional<Board>(u => u.Board)
               .WithMany(u => u.Permissions)
               .WillCascadeOnDelete(true);

            
        }
    }
}
