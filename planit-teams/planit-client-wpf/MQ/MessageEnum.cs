using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public enum MessageEnum
    {
        Error,
        BoardCreate,
        BoardDelete,
        BoardBoardNotification,
        NotificationMove,
        NotificationChange,
        CardListCreate,
        CardListDelete,
        PermissionCreate,
        PermissionDelete,
        CardCreate,
        CardDelete,
        CommentCreate
    }
}
