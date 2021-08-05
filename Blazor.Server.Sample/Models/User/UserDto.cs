using System;
using Blazor.Server.Sample.Enums;

namespace Blazor.Server.Sample.Models.User
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }

    }
}
