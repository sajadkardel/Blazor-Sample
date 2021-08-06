using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.Product;

namespace Blazor.Server.Sample.Data.Product
{
    public interface IProductClientService
    {
        Task<ApiResult<List<ProductDto>>> GetAllProduct();

        Task<ApiResult<ProductDto>> GetByIdProduct(int id);

        Task<ApiResult<ProductDto>> AddProduct(ProductDto dto);

        Task<ApiResult<ProductDto>> UpdateByIdProduct(int id, ProductDto dto);

        Task<ApiResult> RemoveByIdProduct(int id);
    }
}
