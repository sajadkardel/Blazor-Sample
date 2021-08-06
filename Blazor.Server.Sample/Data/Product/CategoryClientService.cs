using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.Product;
using Blazor.Server.Sample.Models.User;
using Newtonsoft.Json;

namespace Blazor.Server.Sample.Data.Product
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly HttpClient _httpClient;
        public CategoryClientService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            })
            {
                BaseAddress = new Uri("https://localhost:44314/api/v1")
            };
        }

        public async Task<ApiResult<List<CategoryDto>>> GetAllCategory()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<List<CategoryDto>>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<CategoryDto>> GetByIdCategory(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Category?id={id}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<CategoryDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<CategoryDto>> AddCategory(CategoryDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Category", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<CategoryDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<CategoryDto>> UpdateByIdCategory(int id, CategoryDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Category?id={id}", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<CategoryDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult> RemoveByIdCategory(int id)
        {
            using var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Category?id={id}");

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(resultAsString);
            return apiResult;
        }
    }
}
