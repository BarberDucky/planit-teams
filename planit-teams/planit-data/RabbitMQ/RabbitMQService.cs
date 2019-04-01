using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_data.RabbitMQ
{
    public class RabbitMQService
    {
        public static void DeclareExchange(string exchName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchName, type: "fanout", durable: true);
            }
        }

        public static void PublishToExchange(string exchName, MessageContext message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var jsonMessage = message.Serialize();
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                channel.BasicPublish(exchange: exchName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
