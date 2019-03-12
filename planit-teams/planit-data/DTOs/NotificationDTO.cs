using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planit_data.Entities;

namespace planit_data.DTOs
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

        public ReadNotificationDTO(Notification notification)
        {
            NotificationId = notification.NotificationId;
            TimeStamp = notification.CreationTime;
            IsRead = notification.IsRead;
            CardId = notification.Card.CardId;
            CardName = notification.Card.Name;
            ListName = notification.Card.List.Name;
            BoardName = notification.Card.List.Board.Name;
            Username = notification.CreatedByUser.IdentificationUser.UserName;
            NotificationType = notification.NotificationType;
        }

        public static List<ReadNotificationDTO> FromList(List<Notification> notifList)
        {
            List<ReadNotificationDTO> dtoList = new List<ReadNotificationDTO>();
            foreach (Notification notif in notifList)
            {
                if (notif != null)
                {
                    dtoList.Add(new ReadNotificationDTO(notif));
                }
            }
            return dtoList;
        }
    }
}
