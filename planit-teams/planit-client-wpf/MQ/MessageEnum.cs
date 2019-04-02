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
        BoardUserUpdate,
        BoardDelete,
        BoardBoardNotification,
        NotificationMove,
        NotificationChange,
        CardListCreate,
        CardListUpdate,
        CardListDelete,
        PermissionCreate,
        PermissionDelete,
        CardCreate,
        CardUpdate,
        CardDelete,
        CardMove,
        CommentCreate,
        BoardUpdate,       
    }
}
