﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace planit_client_forms.Services
{
    public class BoardService
    {
        public static async Task<List<JObject>> GetAllBoards(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Board/BoardsByUser/" + userId);
                var jsonString = await response.Content.ReadAsStringAsync();
                var boardArray = JsonConvert.DeserializeObject<List<JObject>>(jsonString);
                //var boardObj = JObject.Parse(jsonString);
                return boardArray;
            }
        }

        public static async Task<JObject> GetBoard(string id, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:52816/api/Board/{id}/User/{userId}");

                var jsonString = await response.Content.ReadAsStringAsync();
                if (jsonString != "null")
                {
                    var boardObj = JObject.Parse(jsonString);
                    return boardObj;
                }
                else
                {
                    return null;

                }
            }
        }

        public static async Task PutBoard(string id, string newName, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = "{ \"BoardId\":\"" + id + "\"," +
                    "\"Name\":\"" + newName + "\"}";
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync("http://localhost:52816/api/Board/" + userId, byteContent);
            }
        }
    }
}
