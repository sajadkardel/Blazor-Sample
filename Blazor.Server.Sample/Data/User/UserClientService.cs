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
                BaseAddress = new Uri("https://localhost:44314/api")
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

            var httpResponseMessage = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/v1/Users/Token", content);

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<AccessToken>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<List<UserDto>>> GetAllUsersAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/v1/Users");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<List<UserDto>>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> GetByIdUser(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/v1/Users?id={id}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> GetByUserName(string userName)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/v1/Users?userName={userName}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<UserDto>> AddUserAsync(UserDto userDto)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}/v1/Users");
            var json = JsonConvert.SerializeObject(userDto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;

            using var response = await _httpClient.SendAsync(request);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<UserDto>>(resultAsString);
            return apiResult;

        }

        public async Task DeleteUserByUserName(string userName)
        {
            await _httpClient.GetAsync($"{_httpClient.BaseAddress}/v1/Users?userName={userName}");
        }
    }
}
