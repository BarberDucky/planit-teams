using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.MQ
{
    public enum MessageType
    {
        Board,
        CardList,
        Card,
        Comment,
        Error
    }
    public class Message
    {
        public MessageType MessageType { get; set; }
        public int ObjectId { get; set; }

        public override string ToString()
        {
            return $"Promena na: {MessageType}, id objekta: {ObjectId}";
        }
    }
}
