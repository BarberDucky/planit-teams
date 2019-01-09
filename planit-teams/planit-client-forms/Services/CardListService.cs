using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_forms.Services
{
    public class CardListService
    {
        public static async Task<List<JObject>> GetAllCardLists()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/CardList");
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardListArray = JsonConvert.DeserializeObject<List<JObject>>(jsonString);
                return cardListArray;
            }
        }

        public static async Task<JObject> GetCardList(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/CardList/" + id);
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardList = JObject.Parse(jsonString);
                return cardList;
            }
        }

        public static async Task PostCardList(string name, string color, string boardId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = "{ \"Color\":\"" + color + "\"," +
                    "\"Name\":\"" + name + "\"," +
                    "\"BoardId\":\"" + boardId + "\"" +
                    "}";
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("http://localhost:52816/api/CardList", byteContent);
            }
        }
    }
}
