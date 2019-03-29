using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public interface IMessageBroker: IDisposable
    {
        void Publish(object message, MessageEnum mesageType);
        void Subscribe(Action<object> subscription, MessageEnum messageType);
        void Unsubscribe(Action<object> subscription, MessageEnum messageType);
    }
}
