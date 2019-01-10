using planit_data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.RabbitMQ
{
    public enum MessageType
    {
        Board,
        CardList,
        Card,
        Comment
    }
    public class Message
    {
        public MessageType MessageType { get; set; }
        public int ObjectId { get; set; }
    }

    public class BoardMesage
    {
        public MessageType MessageType { get; set; }
        public ReadBoardDTO Data { get; set; }

        public BoardMesage()
        {
            MessageType = MessageType.Board;
        }
    }

    public class CardListMessage
    {
        public MessageType MessageType { get; set; }
        public ReadCardListDTO Data { get; set; }

        public CardListMessage()
        {
            MessageType = MessageType.CardList;
        }
    }

    public class CardMessage
    {
        public MessageType MessageType { get; set; }
        public ReadCardDTO Data { get; set; }

        public CardMessage()
        {
            MessageType = MessageType.Card;
        }
    }
}
