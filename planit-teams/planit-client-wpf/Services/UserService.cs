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
    public class UserService
    {
        public static async Task<bool> RegisterUser(CreateUserDTO createUserData)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // sending of register data
                    string json = JsonConvert.SerializeObject(createUserData);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("http://localhost:52816/api/UserApi/", byteContent);

                    // parsing response
                    var jsonString = await response.Content.ReadAsStringAsync();
                    bool registerResponse = JsonConvert.DeserializeObject<bool>(jsonString);
                    return registerResponse;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static async Task<TokenUserDTO> LoginUser(CredentialsUserDTO credentialsUserDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // sending of register data
                    string encoded_url =
                        "grant_type=" + credentialsUserDTO.grant_type + "&" +
                        "username=" + credentialsUserDTO.username + "&" +
                        "password=" + credentialsUserDTO.password;

                    var buffer = System.Text.Encoding.UTF8.GetBytes(encoded_url);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    var response = await client.PostAsync("http://localhost:52816/Token", byteContent);

                    // parsing response
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        TokenUserDTO tokenResponse = JsonConvert.DeserializeObject<TokenUserDTO>(jsonString);
                        tokenResponse.success = true;
                        return tokenResponse;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static async Task<ReadUserDTO> GetUser(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.GetAsync("http://localhost:52816/api/UserApi");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<ReadUserDTO>(jsonString);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static async Task<List<ReadNotificationDTO>> GetUserNotifications(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.GetAsync("http://localhost:52816/api/UserApi/UserNotifications");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var notifications = JsonConvert.DeserializeObject<List<ReadNotificationDTO>>(jsonString);
                        return notifications;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
