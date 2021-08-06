using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.Product;
using Newtonsoft.Json;

namespace Blazor.Server.Sample.Data.Product
{
    public class ProductClientService : IProductClientService
    {
        private readonly HttpClient _httpClient;
        public ProductClientService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            })
            {
                BaseAddress = new Uri("https://localhost:44314/api/v1")
            };
        }

        public async Task<ApiResult<List<ProductDto>>> GetAllProduct()
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<List<ProductDto>>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<ProductDto>> GetByIdProduct(int id)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Product?id={id}");

            var resultAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<ProductDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<ProductDto>> AddProduct(ProductDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Product", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<ProductDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult<ProductDto>> UpdateByIdProduct(int id, ProductDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Product?id={id}", stringContent);

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult<ProductDto>>(resultAsString);
            return apiResult;
        }

        public async Task<ApiResult> RemoveByIdProduct(int id)
        {
            using var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Product?id={id}");

            var resultAsString = await response.Content.ReadAsStringAsync();
            var apiResult = JsonConvert.DeserializeObject<ApiResult>(resultAsString);
            return apiResult;
        }
    }
}
