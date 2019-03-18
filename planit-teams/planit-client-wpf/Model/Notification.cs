using planit_client_wpf.Base;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Model
{
    public enum NotificationType
    {
        Move,
        Change
    }

    public class Notification : BindableBase
    {
        private bool isRead;

        #region Properties

        public int NotificationId { get; set; }
        public DateTime TimeStamp { get; set; }
        public NotificationType NotificationType { get; set; }
        public int CardId { get; set; }
        public String CardName { get; set; }
        public String ListName { get; set; }
        public String BoardName { get; set; }
        public String Username { get; set; }

        public bool IsRead
        {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }

        #endregion

        public Notification() { }

        public Notification(ReadNotificationDTO dto)
        {
            NotificationId = dto.NotificationId;
            TimeStamp = dto.TimeStamp;
            NotificationType = dto.NotificationType;
            CardId = dto.CardId;
            CardName = dto.CardName;
            ListName = dto.ListName;
            BoardName = dto.BoardName;
            Username = dto.Username;
            IsRead = dto.IsRead;
        }
    }
}
