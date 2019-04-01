using planit_client_wpf.Model;
using planit_client_wpf.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.MQ
{
    public class MQService
    {
        private IConnection connection;
        private IModel channel;
        private ConnectionFactory factory;
        private Dictionary<string, string> tags;

        private static MQService instance;

        public static MQService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MQService();
                }

                return instance;
            }
        }

        private MQService()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            tags = new Dictionary<string, string>();
        }

        //public void SubscribeToExchange(string exchangeName, Func<IMQMessage, bool> Method)
        //{
        //    //channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

        //    var queueName = channel.QueueDeclare().QueueName;

        //    channel.QueueBind(queue: queueName,
        //                      exchange: exchangeName,
        //                      routingKey: "");

        //    var consumer = new EventingBasicConsumer(channel);
        //    consumer.Received += (model, ea) =>
        //    {
        //        var body = ea.Body;
        //        var message = Encoding.UTF8.GetString(body);
        //        IMQMessage msgObj = JsonHelper.GetMessage(message);
        //        Method(msgObj);
        //    };
        //    channel.BasicConsume(queue: queueName,
        //                         autoAck: true,
        //                         consumer: consumer);
        //}

        public void SubscribeToExchange(string exchangeName)
        {
            if (tags.ContainsKey(exchangeName))
                return;

            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,
                              exchange: exchangeName,
                              routingKey: "");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                MQMessage msg = JsonHelper.GetMessageTestConverter(message);
                //MQMessage msgTest = JsonHelper.GetMessageTestConverter(message);

                if (msg != null && msg.Username != ActiveUser.Instance.LoggedUser.Username)
                {
                    MessageEnum msgEnum = msg.GetEnum();

                    if (msgEnum != MessageEnum.Error)
                    {
                        MessageBroker.Instance.Publish(msg.GetData(), msgEnum);
                    }
                }

            };

            string tag = channel.BasicConsume(queue: queueName,
                                  autoAck: true,
                                  consumer: consumer);

            tags.Add(exchangeName, tag);
        }

        public void Unsubscribe(string exchangeName)
        {
            if (exchangeName == null || !tags.ContainsKey(exchangeName))
                return;

            string tag = tags[exchangeName];

            channel.BasicCancel(tag);

            tags.Remove(exchangeName);
        }

        public void UnsubscribeFromAll()
        {
            foreach (var val in tags.Values)
            {
                channel.BasicCancel(val);
            }

            tags.Clear();
        }
    }
}
