using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public enum NotificationType
    {
        Move,
        Change
    }

    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public DateTime CreationTime { get; protected set; }

        [DefaultValue("false")]
        public bool IsRead { get; set; }

        public NotificationType NotificationType { get; set; }
 
        public virtual int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        public virtual int BelongsToUserId { get; set; }
        public virtual User BelongsToUser { get; set; }
 
        public virtual Card Card { get; set; }
        
        public Notification()
        {
            CreationTime = DateTime.Now;
        }
    }
}
