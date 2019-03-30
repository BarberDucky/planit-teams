using Newtonsoft.Json;
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
    public class NotificationService
    {
        public static async Task<bool> ReadNotification(string accessToken, int notificationId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PutAsync("http://localhost:52816/api/Notification/Read/" + notificationId, null);

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

        public static async Task<bool> ReadAllNotifications(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PutAsync("http://localhost:52816/api/Notification/ReadAll", null);

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
