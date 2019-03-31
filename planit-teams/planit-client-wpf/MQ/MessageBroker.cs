using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using planit_client_wpf.DTOs;

namespace planit_client_wpf.MQ
{
    public class MessageBroker : IMessageBroker
    {
        private static MessageBroker instance;
        private Dictionary<MessageEnum, Action<object>> dictionary;

        public static MessageBroker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageBroker();
                }

                return instance;
            }
        }

        private MessageBroker()
        {
            dictionary = new Dictionary<MessageEnum, Action<object>>();
        }

        public void Publish(object message, MessageEnum messageType)
        {
            if (message == null || !dictionary.ContainsKey(messageType))
                return;

            Action<object> action = dictionary[messageType];

            if (action == null)
                return;

            if (Application.Current == null)
                return;

            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, action, message);
        }

        public void Subscribe(Action<object> subscription, MessageEnum messageType)
        {
            if (subscription == null)
                return;

            if (!dictionary.ContainsKey(messageType))
            {
                dictionary.Add(messageType, subscription);
            }
            else
            {
                dictionary[messageType] += subscription;
            }
        }

        public void Unsubscribe(MessageEnum messageType)
        {
            if (!dictionary.ContainsKey(messageType))
                return;

            var delegates = dictionary[messageType];

            if (delegates == null)
                return;

            dictionary.Remove(messageType);
        }

        public void Dispose()
        {
            dictionary?.Clear();
        }

        public void UnsubscribeStartingFrom(MessageEnum start)
        {
            var keys = dictionary.Keys.ToArray();
            foreach (var key in keys)
            {
                if (key >= start)
                {
                    dictionary.Remove(key);
                }
            }
        }
    }
}
