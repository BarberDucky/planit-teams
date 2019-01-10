using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace planit_client_forms.MQ
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
        public void SubscribeToExchange(string exchangeName, Func<object, bool> Method)
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: exchangeName,
                              routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var jsonMessage = Encoding.UTF8.GetString(body);
                var messageObj = JsonConvert.DeserializeObject<Message>(jsonMessage);
                Method(messageObj.ToString());
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}

