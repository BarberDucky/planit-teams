using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public DateTime CreationTime { get; set; }
        public bool IsRead { get; set; }

        public virtual User User { get; set; }
        public virtual Card Card { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Notification()
        {
            Users = new HashSet<User>();
        }
    }
}
