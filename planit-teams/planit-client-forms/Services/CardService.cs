using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using planit_client_forms.DTOs;

namespace planit_client_forms.Services
{
    public class CardService
    {
        public static async Task<List<ReadCardDTO>> GetAllCards(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Card");
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardArray = JsonConvert.DeserializeObject<List<ReadCardDTO>>(jsonString);
                return cardArray;
            }
        }

        public static async Task<ReadCardDTO> GetCard(string id, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Card/" + id);
                var jsonString = await response.Content.ReadAsStringAsync();
                var card = JsonConvert.DeserializeObject<ReadCardDTO>(jsonString);
                return card;
            }
        }

        public static async Task PostCard(CreateCardDTO createData, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(createData);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("http://localhost:52816/api/Card", byteContent);
            }
        }
    }
}
