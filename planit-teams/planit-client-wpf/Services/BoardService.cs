﻿using Newtonsoft.Json;
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
    public class BoardService
    {
        public static async Task<List<ShortBoardDTO>> GetBoardsByUser(string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.GetAsync("http://localhost:52816/api/Board/BoardsByUser/");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var boardArray = JsonConvert.DeserializeObject<List<ShortBoardDTO>>(jsonString);
                        return boardArray;
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

        public static async Task<ReadBoardDTO> GetBoard(string accessToken, int boardId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string id = boardId.ToString();
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.GetAsync("http://localhost:52816/api/Board/" + id);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var board = JsonConvert.DeserializeObject<ReadBoardDTO>(jsonString);
                        return board;
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

        public static async Task<ShortBoardDTO> CreateBoard(string accessToken, CreateBoardDTO createBoardDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(createBoardDTO);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PostAsync("http://localhost:52816/api/Board/", byteContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var boardArray = JsonConvert.DeserializeObject<ShortBoardDTO>(jsonString);
                        return boardArray;
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

        public static async Task<bool> UpdateBoard(string accessToken, int boardId, UpdateBoardDTO updateBoardDTO)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(updateBoardDTO);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.PostAsync("http://localhost:52816/api/Board/" + boardId, byteContent);

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

        public static async Task<bool> DeleteBoard(string accessToken, int boardId)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string id = boardId.ToString();
                    client.DefaultRequestHeaders.Add("Authorization", accessToken);
                    var response = await client.DeleteAsync("http://localhost:52816/api/Board/" + id);
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
