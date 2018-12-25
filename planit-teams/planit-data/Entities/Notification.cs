using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public DateTime CreationTime { get; protected set; }
        [DefaultValue("false")]
        public bool IsRead { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual Card Card { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Notification()
        {
            Users = new HashSet<User>();
            CreationTime = DateTime.Now;
        }
    }
}
