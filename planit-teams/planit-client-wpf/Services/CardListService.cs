using Newtonsoft.Json;
using planit_client_wpf.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Services
{
    public class CardListService
    {
        public static async Task<ReadCardListDTO> GetCardList(string accessToken, int cardListId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.GetAsync("http://localhost:52816/api/CardList/" + cardListId);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var cardList = JsonConvert.DeserializeObject<ReadCardListDTO>(jsonString);
                        return cardList;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return null;
                }

            }
        }

        public static async Task<BasicCardListDTO> CreateCardList(string accessToken, CreateCardListDTO createListDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(createListDTO);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PostAsync("http://localhost:52816/api/CardList/", byteContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var cardList = JsonConvert.DeserializeObject<BasicCardListDTO>(jsonString);
                        return cardList;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public static async Task<bool> UpdateCardList(string accessToken, int cardListId, UpdateCardListDTO updateListDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(updateListDTO);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PutAsync("http://localhost:52816/api/CardList/" + cardListId, byteContent);

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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
        }

        public static async Task<bool> DeleteCardList(string accessToken, int cardListId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.DeleteAsync("http://localhost:52816/api/CardList/" + cardListId);
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
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
        }
    }
}
