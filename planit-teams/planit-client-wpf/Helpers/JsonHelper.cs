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
        #region Should Delete

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

            var messageUsername = GetJTokenByKey("Username", jsonMessage);

            var data = GetJTokenByKey("Data", jsonMessage);
            if (data == null)
                return null;

            var stringData = data.ToString();

            var type = (MessageType)messageType.Value<int>();
            var entity = (MessageEntity)messageEntity.Value<int>();
            var username = (string)messageUsername.Value<string>();

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
                return new CardListMessage() { MessageType = type, MessageEntity = entity, Data = parsedData, Username = username };
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

        public static MQMessage GetMessageTest(string stringMessage)
        {
            BasicMQMessage msg = JsonConvert.DeserializeObject<BasicMQMessage>(stringMessage);

            if (msg.MessageEntity == MessageEntity.Permission)
            {
                return JsonConvert.DeserializeObject<PermissionMessage>(stringMessage);

            }
            else if (msg.MessageType == MessageType.Delete)
            {
                return JsonConvert.DeserializeObject<DeleteMessage>(stringMessage);
            }
            else if (msg.MessageType == MessageType.BoardNotification)
            {
                return JsonConvert.DeserializeObject<BoardNotificationMessage>(stringMessage);
            }
            else if (msg.MessageEntity == MessageEntity.Board)
            {
                return JsonConvert.DeserializeObject<BoardMesage>(stringMessage);
            }
            else if (msg.MessageEntity == MessageEntity.Card)
            {
                return JsonConvert.DeserializeObject<CardMessage>(stringMessage);
            }
            else if (msg.MessageEntity == MessageEntity.CardList)
            {
                return JsonConvert.DeserializeObject<CardListMessage>(stringMessage);
            }
            else if (msg.MessageEntity == MessageEntity.Comment)
            {
                return JsonConvert.DeserializeObject<CommentMessage>(stringMessage);
            }
            else if (msg.MessageEntity == MessageEntity.Notification)
            {
                return JsonConvert.DeserializeObject<NotificationMessage>(stringMessage);
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
        #endregion


        public static MQMessage ConvertJsonToMessage(string json)
        {
            return JsonConvert.DeserializeObject<MQMessage>(json, new JsonMessageConverter());
        }    
    }
}
