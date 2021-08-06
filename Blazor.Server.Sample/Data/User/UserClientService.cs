using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.User;
using Newtonsoft.Json;

namespace Blazor.Server.Sample.Data.User
{
    public class UserClientService : IUserClientService
    {
        private readonly HttpClient _httpClient;
        public UserClientService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            })
            {
                BaseAddress = new Uri("https://localhost:44314/api/v1")
            };
        }


        public async Task<ApiResult<AccessToken>> Token(TokenRequest tokenRequest)
        {
            var content = new FormUrlEncodedContent(new[]
            {
               new KeyValuePair<string, string>("userNAme" , tokenRequest.username),
               new KeyValuePair<string, string>("password",tokenRequest.password),
               new KeyValuePair<string, string>("grant_type",tokenRequest.grant_type)
            });

            var httpResponseMessage = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Users/Token", content);

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<AccessToken>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllUsersAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Users");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<List<UserDto>>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> GetByIdUser(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Users?id={id}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> GetByUserName(string userName)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Users/GetByUserName?userName={userName}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> AddUserAsync(UserDto userDto)
        {
            var json = JsonConvert.SerializeObject(userDto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Users", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;

        }

        public async Task<ApiResult> DeleteUserByUserName(string userName)
        {
            using var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Users/DeleteByUserName?userName={userName}");

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> UpdateUserByUserName(string userName, UserDto userDto)
        {
            var json = JsonConvert.SerializeObject(userDto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Users/UpdateWithGetByUserName?userName={userName}", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;
        }
    }
}
