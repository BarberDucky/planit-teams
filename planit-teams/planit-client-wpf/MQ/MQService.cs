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

        public IModel Channel
        {
            get
            {
                return channel;
            }
        }

        public MQService()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

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
            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queue: queueName,
                              exchange: exchangeName,
                              routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                MQMessage msg = JsonHelper.GetMessage(message);

                MessageEnum msgEnum = msg.GetEnum();

                if (msgEnum != MessageEnum.Error)
                {
                    MessageBroker.Instance.Publish(msg.GetData(), msgEnum);
                }

            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
