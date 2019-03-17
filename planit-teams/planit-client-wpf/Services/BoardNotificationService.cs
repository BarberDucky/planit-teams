using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace planit_client_wpf.Services
{
    public class BoardNotificationService
    {
        public static async Task<bool> ReadBoardNotification(string accessToken, int boardId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", accessToken);
                var response = await client.PostAsync("http://localhost:52816/api/BoardNotification/Board/" + boardId, null);

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
