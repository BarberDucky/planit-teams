using Newtonsoft.Json;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Services
{
    public class CardService
    {
        public static async Task<ReadCardDTO> GetCard(string accessToken, int cardId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.GetAsync("http://localhost:52816/api/Card/" + cardId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var card = JsonConvert.DeserializeObject<ReadCardDTO>(jsonString);
                    return card;
                }
                else
                {
                    return null;
                }

            }
        }

        public static async Task<BasicCardDTO> CreateCard(string accessToken, CreateCardDTO createCardDTO)
        {
            using (HttpClient client = new HttpClient())
            {

                string json = JsonConvert.SerializeObject(createCardDTO);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.PostAsync("http://localhost:52816/api/Card/", byteContent);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var card = JsonConvert.DeserializeObject<BasicCardDTO>(jsonString);
                    return card;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<bool> UpdateCard(string accessToken, int cardId, UpdateCardDTO updateCardDTO)
        {
            using (HttpClient client = new HttpClient())
            {

                string json = JsonConvert.SerializeObject(updateCardDTO);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.PostAsync("http://localhost:52816/api/Card/" + cardId, byteContent);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(jsonString);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> MoveCard(string accessToken, int cardId, int cardListId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.PutAsync("http://localhost:52816/api/Card/" + cardId + "/Move/" + cardListId, null);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(jsonString);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> DeleteCard(string accessToken, int cardId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.DeleteAsync("http://localhost:52816/api/Card/" + cardId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(jsonString);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> WatchCard(string accessToken, int cardId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.GetAsync("http://localhost:52816/api/Card/WatchCard/" + cardId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(jsonString);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }

        public static async Task<bool> UnwatchCard(string accessToken, int cardId)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.GetAsync("http://localhost:52816/api/Card/UnwatchCard/" + cardId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(jsonString);
                    return result;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
