using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public enum MessageEntity
    {
        Board,
        CardList,
        Card,
        Comment,
        Notification
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

    public interface IMQMessage
    {
    }

    public class BoardMesage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public BasicBoardDTO Data { get; set; }

        public BoardMesage()
        {
            MessageEntity = MessageEntity.Board;
        }
    }

    public class CardListMessage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public BasicCardListDTO Data { get; set; }

        public CardListMessage()
        {
            MessageEntity = MessageEntity.CardList;
        }
    }

    public class CardMessage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public BasicCardDTO Data { get; set; }

        public CardMessage()
        {
            MessageEntity = MessageEntity.Card;
        }
    }

    public class CommentMessage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public BasicCommentDTO Data { get; set; }

        public CommentMessage()
        {
            MessageEntity = MessageEntity.Comment;
        }
    }

    public class NotificationMessage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public ReadNotificationDTO Data { get; set; }

        public NotificationMessage()
        {
            MessageEntity = MessageEntity.Notification;
        }
    }

    public class BoardNotificationMessage : IMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public int Data { get; set; }

        public BoardNotificationMessage()
        {
            MessageEntity = MessageEntity.Board;
            MessageType = MessageType.BoardNotification;
        }
    }

    public class DeleteMessage : IMQMessage
    {
        public MessageEntity MessageEntity;
        public MessageType MessageType;
        public int Data;

        public DeleteMessage()
        {
            MessageType = MessageType.Delete;
        }
    }
}
