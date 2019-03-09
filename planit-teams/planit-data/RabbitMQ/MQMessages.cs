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
        Notification
    }

    public enum MessageType
    {
        Create,
        Update,
        Move,
        Delete
    }
   
    public class BoardMesage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public ReadBoardDTO Data { get; set; }

        public BoardMesage()
        {
            MessageEntity = MessageEntity.Board;
        }
    }

    public class CardListMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public ReadCardListDTO Data { get; set; }

        public CardListMessage()
        {
            MessageEntity = MessageEntity.CardList;
        }
    }

    public class CardMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public ReadCardDTO Data { get; set; }

        public CardMessage()
        {
            MessageEntity = MessageEntity.Card;
        }
    }

    public class CommentMessage
    {
        public MessageEntity MessageEntity { get; set; }
        public MessageType MessageType { get; set; }
        public ReadCommentDTO Data { get; set; }

        public CommentMessage()
        {
            MessageEntity = MessageEntity.Comment;
        }
    }

    public class DeleteMessage
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
