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
}
