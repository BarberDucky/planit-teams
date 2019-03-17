using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_client_wpf.Model;

namespace planit_client_wpf.DTOs
{
    public class CreateNotificationDTO
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public NotificationType NotificationType { get; set; }
    }

    public class ReadNotificationDTO
    {
        public int NotificationId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public NotificationType NotificationType { get; set; }
        public int CardId { get; set; }
        public String CardName { get; set; }
        public String ListName { get; set; }
        public String BoardName { get; set; }
        public String Username { get; set; }
    }
}
