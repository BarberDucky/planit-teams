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
    public class CardListService
    {
        public static async Task<List<ReadCardListDTO>> GetAllCardLists(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/CardList");
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardListArray = JsonConvert.DeserializeObject<List<ReadCardListDTO>>(jsonString);
                return cardListArray;
            }
        }

        public static async Task<ReadCardListDTO> GetCardList(string id, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/CardList/" + id);
                var jsonString = await response.Content.ReadAsStringAsync();
                var cardList = JsonConvert.DeserializeObject<ReadCardListDTO>(jsonString);
                return cardList;
            }
        }

        public static async Task PostCardList(CreateCardListDTO createData, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(createData);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("http://localhost:52816/api/CardList", byteContent);
            }
        }
    }
}
