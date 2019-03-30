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
            if (subscription != null && !dictionary.ContainsKey(messageType))
            {
                dictionary.Add(messageType, subscription);
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

        //#region IDisposable Support
        //private bool disposedValue = false; // To detect redundant calls

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects).
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // TODO: set large fields to null.

        //        disposedValue = true;
        //    }
        //}

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //// ~MessageBroker() {
        ////   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        ////   Dispose(false);
        //// }

        //// This code added to correctly implement the disposable pattern.
        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    Dispose(true);
        //    // TODO: uncomment the following line if the finalizer is overridden above.
        //    // GC.SuppressFinalize(this);
        //}
        //#endregion
    }
}
