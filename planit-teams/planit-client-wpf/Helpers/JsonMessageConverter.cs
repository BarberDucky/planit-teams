using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using planit_client_wpf.MQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Helpers
{
    public class JsonMessageConverter : Newtonsoft.Json.Converters.CustomCreationConverter<MQMessage>
    {
        public override MQMessage Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public MQMessage Create(Type objectType, JObject jObject)
        {
            var type = (MessageType)jObject.Value<int>("MessageType");
            var entity = (MessageEntity)jObject.Value<int>("MessageEntity");

            if (entity == MessageEntity.Permission)
            {
                return new PermissionMessage();
            }
            else if (type == MessageType.Delete)
            {
                return new DeleteMessage();
            }
            else if (type == MessageType.BoardNotification)
            {
                return new BoardNotificationMessage();
            }
            else if (entity == MessageEntity.Board)
            {
                return new BoardMesage();
            }
            else if (entity == MessageEntity.Card)
            {
                return new CardMessage();
            }
            else if (entity == MessageEntity.CardList)
            {
                return new CardListMessage();
            }
            else if (entity == MessageEntity.Comment)
            {
                return new CommentMessage();
            }
            else if (entity == MessageEntity.Notification)
            {
                return new NotificationMessage();
            }
            else
            {
                return null;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jobject = JObject.Load(reader);

            var target = Create(objectType, jobject);

            serializer.Populate(jobject.CreateReader(), target);

            return target;
        }
    }
}
