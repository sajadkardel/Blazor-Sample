using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<ApiResult<List<UserSelectDto>>> GetAllUsersAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/v1/Users");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<List<UserSelectDto>>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult> AddUserAsync(UserDto userDto)
        {
            var toJson = JsonConvert.SerializeObject(userDto);
            var buffer = System.Text.Encoding.UTF8.GetBytes(toJson);
            HttpContent byteContent = new ByteArrayContent(buffer);
            var httpResponseMessage =  await _httpClient.PostAsync($"{_httpClient.BaseAddress}/v1/Users", byteContent);

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(resultAsString);
            return apiResult;
        }
    }
}
