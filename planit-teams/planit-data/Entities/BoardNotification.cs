using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.Entities
{
    public enum BoardNotificationType
    {

    }

    public class BoardNotification
    {
        [Key]
        public int BoardNotificationId { get; set; }

        [DefaultValue(true)]
        public bool IsRead { get; set; }

        public BoardNotificationType BoardNotificationType { get; set; }

        public virtual Board Board { get; set; }

        public virtual User User { get; set; }
    }
}
