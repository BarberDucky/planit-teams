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
    public class CardService
    {
        public static async Task<List<JObject>> GetAllCards()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Card");
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardArray = JsonConvert.DeserializeObject<List<JObject>>(jsonString);
                return cardArray;
            }
        }

        public static async Task<JObject> GetCard(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Card/" + id);
                var jsonString = await response.Content.ReadAsStringAsync();
                var card = JObject.Parse(jsonString);
                return card;
            }
        }

        public static async Task PostCardList(string name, string description, string listId, string userId, DateTime dueDate)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = "{ \"Name\":\"" + name + "\"," +
                    "\"Description\":\"" + description + "\"," +
                    "\"ListId\":\"" + listId + "\"," +
                    "\"UserId\":\"" + userId + "\"," +
                    "\"DueDate\":\"" + dueDate.ToShortDateString() + "\"" +
                    "}";
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("http://localhost:52816/api/Card", byteContent);
            }
        }
    }
}
