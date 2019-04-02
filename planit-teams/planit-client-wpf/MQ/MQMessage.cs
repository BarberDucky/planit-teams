using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Change,
        UserUpdate
    }

    public interface IMQMessage
    {

    }

    public class BasicMQMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public string Username { get; set; }
    }

    public abstract class MQMessage : BasicMQMessage
    {
        //public MessageEntity MessageEntity { get; set; }
        //public MessageType MessageType { get; set; }
        //public string Username { get; set; }

        public MessageEnum GetEnum()
        {
            string entity = MessageEntity.ToString();
            string type = MessageType.ToString();

            string name = $"{entity}{type}";

            try
            {
                MessageEnum msgEnum = (MessageEnum)Enum.Parse(typeof(MessageEnum), name);
                return msgEnum;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return MessageEnum.Error;
            }

        }

        public abstract object GetData();
    }

    public class BoardMesage : MQMessage
    {
        public BasicBoardDTO Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class CardListMessage : MQMessage
    {
        public BasicCardListDTO Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class CardMessage : MQMessage
    {
        public BasicCardDTO Data { get; set; }

        public CardMessage()
        {
            MessageEntity = MessageEntity.Card;
        }

        public override object GetData()
        {
            return Data;
        }
    }

    public class CommentMessage : MQMessage
    {
        public BasicCommentDTO Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class NotificationMessage : MQMessage
    {
        public ReadNotificationDTO Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class BoardNotificationMessage : MQMessage
    {
        public int Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class PermissionMessage : MQMessage
    {
        public ReadUserDTO Data { get; set; }

        public override object GetData()
        {
            return Data;
        }
    }

    public class DeleteMessage : MQMessage
    {
        public int Data;

        public override object GetData()
        {
            return Data;
        }
    }
}
