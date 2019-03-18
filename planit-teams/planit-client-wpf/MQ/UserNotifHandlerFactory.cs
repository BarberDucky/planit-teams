using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public class UserNotifHandlerFactory
    {
        public static IUserNotificationHandler CreateHandler(IMQMessage message)
        {
            if (message is BoardMesage && ((BoardMesage)message).MessageType == MessageType.Create)
            {
                return new GivePermissionHandler();
            } 
            else if (message is DeleteMessage && ((DeleteMessage)message).MessageEntity == MessageEntity.Board)
            {
                return new DeletePermissionHandler();
            }
            else if (message is BoardNotificationMessage && ((BoardNotificationMessage)message).MessageEntity == MessageEntity.Board)
            {
                return new BoardChangeHandler();
            }
            else if (message is NotificationMessage && ((NotificationMessage)message).MessageType == MessageType.Move)
            {
                return new CardMoveHandler();
            }
            else if (message is NotificationMessage && ((NotificationMessage)message).MessageType == MessageType.Change)
            {
                return new WatchedCardHandler();
            }
            else
            {
                return null;
            }
        }
    }
}
