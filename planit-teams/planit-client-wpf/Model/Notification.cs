using planit_client_wpf.Base;
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
    }
}
