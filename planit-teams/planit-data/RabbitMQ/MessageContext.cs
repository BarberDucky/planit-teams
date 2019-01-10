using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.RabbitMQ
{
    public class MessageContext
    {
        private IMessageStrategy messageStrategy;

        public MessageContext(IMessageStrategy strategy)
        {
            this.messageStrategy = strategy;
        }

        public string Serialize()
        {
            return messageStrategy.Serialize();
        }
    }
}
