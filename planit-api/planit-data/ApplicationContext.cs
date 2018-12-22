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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>()
                .HasMany<User>(u => u.Users)
                .WithMany(c => c.Notifications)
                .Map(cs =>
                {
                    cs.MapLeftKey("NotificationRefId");
                    cs.MapRightKey("UserRefId");
                    cs.ToTable("NotificationUser");
                });

            modelBuilder.Entity<Card>()
                .HasMany<User>(u => u.ObserverUsers)
                .WithMany(c => c.WatchedCards)
                .Map(cs =>
                {
                    cs.MapLeftKey("CardRefId");
                    cs.MapRightKey("UserRefId");
                    cs.ToTable("CardObserverUser");
                });
        }
    }
}
