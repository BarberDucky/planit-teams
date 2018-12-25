using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public virtual ApplicationUser IdentificationUser { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Card> WatchedCards { get; set; }

        public User()
        {
            Permissions = new HashSet<Permission>();
            Cards = new HashSet<Card>();
            Notifications = new HashSet<Notification>();
            WatchedCards = new HashSet<Card>();
        }
    }
}
