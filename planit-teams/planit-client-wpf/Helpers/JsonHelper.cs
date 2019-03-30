using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using planit_client_wpf.DTOs;
using planit_client_wpf.MQ;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Helpers
{
    public class JsonHelper
    {
        public static MQMessage GetMessage(string stringMessage)
        {
            var jsonMessage = GetJObjectFromString(stringMessage);
            if (jsonMessage == null)
                return null;

            var messageType = GetJTokenByKey("MessageType", jsonMessage);
            if (messageType == null)
                return null;

            var messageEntity = GetJTokenByKey("MessageEntity", jsonMessage);
            if (messageEntity == null)
                return null;

            var data = GetJTokenByKey("Data", jsonMessage);
            if (data == null)
                return null;

            var stringData = data.ToString();

            var type = (MessageType)messageType.Value<int>();
            var entity = (MessageEntity)messageEntity.Value<int>();

            if (type == MessageType.Delete)
            {
                int parsedData = JsonConvert.DeserializeObject<int>(stringData);
                return new DeleteMessage() { MessageType = MessageType.Delete, MessageEntity = entity, Data = parsedData };
            }
            else if (type == MessageType.BoardNotification)
            {
                int parsedData = JsonConvert.DeserializeObject<int>(stringData);
                return new BoardNotificationMessage() { MessageType = MessageType.BoardNotification, MessageEntity = entity, Data = parsedData };
            }
            else if (entity == MessageEntity.Board)
            {
                BasicBoardDTO parsedData = JsonConvert.DeserializeObject<BasicBoardDTO>(stringData);
                return new BoardMesage() { MessageType = type, MessageEntity = entity, Data = parsedData };
            }
            else if (entity == MessageEntity.Card)
            {
                BasicCardDTO parsedData = JsonConvert.DeserializeObject<BasicCardDTO>(stringData);
                return new CardMessage() { MessageType = type, MessageEntity = entity, Data = parsedData };
            }
            else if (entity == MessageEntity.CardList)
            {
                BasicCardListDTO parsedData = JsonConvert.DeserializeObject<BasicCardListDTO>(stringData);
                return new CardListMessage() { MessageType = type, MessageEntity = entity, Data = parsedData };
            }
            else if (entity == MessageEntity.Comment)
            {
                BasicCommentDTO parsedData = JsonConvert.DeserializeObject<BasicCommentDTO>(stringData);
                return new CommentMessage() { MessageType = type, MessageEntity = entity, Data = parsedData };
            }
            else if (entity == MessageEntity.Notification)
            {
                ReadNotificationDTO parsedData = JsonConvert.DeserializeObject<ReadNotificationDTO>(stringData);
                return new NotificationMessage() { MessageType = type, MessageEntity = entity, Data = parsedData };
            }
            else
            {
                return null;
            }


        }

        private static JToken GetJTokenByKey(string key, JObject obj)
        {
            if (obj.ContainsKey(key))
            {
                return obj[key];
            }
            else
            {
                return null;
            }
        }

        private static JObject GetJObjectFromString(string str)
        {
            try
            {
                return JObject.Parse(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static MessageType GetMessageType(JToken token)
        {
            try
            {
                return (MessageType)token.Value<int>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return MessageType.Change;
            }
        }
    }
}
