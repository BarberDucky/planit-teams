using planit_client_wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public interface IUserNotificationHandler
    {
        void HandleUserNotification(ObservableCollection<ShortBoard> boardList, ShortBoard selectedBoard, ObservableCollection<Notification> notifications, IMQMessage message);
    }
}
