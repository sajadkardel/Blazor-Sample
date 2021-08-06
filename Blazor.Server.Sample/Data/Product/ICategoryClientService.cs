using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.Product;

namespace Blazor.Server.Sample.Data.Product
{
    public interface ICategoryClientService
    {
        Task<ApiResult<List<CategoryDto>>> GetAllCategory();

        Task<ApiResult<CategoryDto>> GetByIdCategory(int id);

        Task<ApiResult<CategoryDto>> AddCategory(CategoryDto dto);

        Task<ApiResult<CategoryDto>> UpdateByIdCategory(int id, CategoryDto dto);

        Task<ApiResult> RemoveByIdCategory(int id);

    }
}
