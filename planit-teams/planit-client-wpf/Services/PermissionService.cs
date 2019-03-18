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
    public class PermissionService
    {
        public static async Task<bool> CreatePermission(string accessToken, AddUserBoardPermisionDTO addUserBoardPermisionDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(addUserBoardPermisionDTO);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PostAsync("http://localhost:52816/api/Permission/", byteContent);

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
                    return false;
                }
            }
        }

        public static async Task<bool> DeletePermission(string accessToken, int boardId, string username)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.DeleteAsync("http://localhost:52816/api/Permission/Board/" + boardId + "/User/" + username);
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
                    return false;
                }
            }
        }
    }
}
