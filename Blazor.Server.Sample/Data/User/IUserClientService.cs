using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.User;

namespace Blazor.Server.Sample.Data.User
{
    public interface IUserClientService
    {
        Task<ApiResult<AccessToken>> Token(TokenRequest tokenRequest);

        Task<ApiResult<List<UserDto>>> GetAllUsersAsync();

        Task<ApiResult<UserDto>> GetByUserName(string userName);

        Task<ApiResult<UserDto>> AddUserAsync(UserDto userDto);

        Task<ApiResult> DeleteUserByUserName(string userName);

        Task<ApiResult<UserDto>> UpdateUserByUserName(string userName , UserDto userDto);
    }
}
