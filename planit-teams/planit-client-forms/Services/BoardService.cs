using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using planit_client_forms.DTOs;

namespace planit_client_forms.Services
{
    public class BoardService
    {
        public static async Task<List<ReadBoardDTO>> GetAllBoards(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:52816/api/Board/BoardsByUser/" + userId);
                var jsonString = await response.Content.ReadAsStringAsync();
                var boardArray = JsonConvert.DeserializeObject<List<ReadBoardDTO>>(jsonString);
                return boardArray;
            }
        }

        public static async Task<ReadBoardDTO> GetBoard(string id, int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:52816/api/Board/{id}/User/{userId}");

                var jsonString = await response.Content.ReadAsStringAsync();
                if (jsonString != "null")
                {
                    var boardObj = JsonConvert.DeserializeObject<ReadBoardDTO>(jsonString);
                    return boardObj;
                }
                else
                {
                    return null;

                }
            }
        }

        public static async Task PutBoard(UpdateBoardDTO updateData, int userId)
        {
            using (HttpClient client = new HttpClient())
            {

                string json = JsonConvert.SerializeObject(updateData);

                var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync("http://localhost:52816/api/Board/" + userId, byteContent);
            }
        }
    }
}
