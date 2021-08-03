using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.Sample.Common;
using Blazor.Server.Sample.Models.User;

namespace Blazor.Server.Sample.Data.User
{
    public interface IUserClientService
    {
        Task<ApiResult<AccessToken>> Token(TokenRequest tokenRequest);

        Task<ApiResult<List<UserSelectDto>>> GetAllUsersAsync();

        Task<ApiResult> AddUserAsync(UserDto userDto);
    }
}
