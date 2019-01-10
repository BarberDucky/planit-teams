using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using planit_client_forms.DTOs;
using planit_client_forms.MQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.Helpers
{
    public class JsonHelper
    {
        public static String GetMessage(string stringMessage)
        {
            var jsonMessage = GetJObjectFromString(stringMessage);
            if (jsonMessage == null)
                return "";

            var messageType = GetJTokenByKey("MessageType", jsonMessage);
            if (messageType == null)
                return "";

            var data = GetJTokenByKey("Data", jsonMessage);
            if (data == null)
                return "";

            var stringData = data.ToString();

            var type = (MessageType)messageType.Value<int>();

            switch (type)
            {
                case MessageType.Board:
                    return JsonConvert.DeserializeObject<ReadBoardDTO>(stringData).ToString();
                case MessageType.Card:
                    return JsonConvert.DeserializeObject<ReadCardDTO>(stringData).ToString();
                case MessageType.CardList:
                    return JsonConvert.DeserializeObject<ReadCardListDTO>(stringData).ToString();
                default:
                    return "";
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
            catch(Exception e)
            {
                return MessageType.Error;
            }
        }
    }
}
