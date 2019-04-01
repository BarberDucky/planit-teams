using planit_data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.RabbitMQ
{
    public enum MessageEntity
    {
        Board,
        CardList,
        Card,
        Comment,
        Notification,
        Permission
    }

    public enum MessageType
    {
        Create,
        Update,
        Move,
        Delete,
        BoardNotification,
        Change
    }

    public class MQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public string Username { get; set; }

        public MQMessage()
        {
            Username = string.Empty;
        }

        public MQMessage(string username)
        {
            this.Username = username;
        }
    }

    public class BoardMesage : MQMessage
    {
        public BasicBoardDTO Data { get; set; }

        public BoardMesage()
        {
            MessageEntity = MessageEntity.Board;
        }

        public BoardMesage(string username)
            : base(username)
        {
            MessageEntity = MessageEntity.Board;
        }
    }

    public class CardListMessage : MQMessage
    {
        public BasicCardListDTO Data { get; set; }

        public CardListMessage(string username)
            : base(username)
        {
            MessageEntity = MessageEntity.CardList;
        }
    }

    public class CardMessage : MQMessage
    {
        public BasicCardDTO Data { get; set; }

        public CardMessage()
        {
            MessageEntity = MessageEntity.Card;
        }

        public CardMessage(string username)
           : base(username)
        {
            MessageEntity = MessageEntity.Card;
        }
    }

    public class CommentMessage : MQMessage
    {
        public BasicCommentDTO Data { get; set; }

        public CommentMessage()
        {
            MessageEntity = MessageEntity.Comment;
        }

        public CommentMessage(string username)
          : base(username)
        {
            MessageEntity = MessageEntity.Comment;
        }
    }

    public class NotificationMessage : MQMessage
    {
        public ReadNotificationDTO Data { get; set; }

        public NotificationMessage()
        {
            MessageEntity = MessageEntity.Notification;
        }

        public NotificationMessage(string username)
          : base(username)
        {
            MessageEntity = MessageEntity.Notification;
        }
    }

    public class BoardNotificationMessage : MQMessage
    {
        public int Data { get; set; }

        public BoardNotificationMessage()
        {
            MessageEntity = MessageEntity.Board;
            MessageType = MessageType.BoardNotification;
        }

        public BoardNotificationMessage(string username)
          : base(username)
        {
            MessageEntity = MessageEntity.Board;
            MessageType = MessageType.BoardNotification;
        }
    }

    public class PermissionMessage : MQMessage
    {
        public ReadUserDTO Data { get; set; }

        public PermissionMessage(string username)
          : base(username)
        {
            MessageEntity = MessageEntity.Permission;
        }
    }

    public class DeleteMessage : MQMessage
    {
        public int Data;

        public DeleteMessage()
        {
            MessageType = MessageType.Delete;
        }

        public DeleteMessage(string username)
         : base(username)
        {
            MessageType = MessageType.Delete;
        }
    }
}
