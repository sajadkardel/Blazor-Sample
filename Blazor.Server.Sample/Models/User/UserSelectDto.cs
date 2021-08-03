using System;
using Blazor.Server.Sample.Enums;
using Newtonsoft.Json;

namespace Blazor.Server.Sample.Models.User
{
    public class UserSelectDto
    {
        [JsonProperty("userName")]
        public virtual string UserName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public virtual string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public virtual string PhoneNumber { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("gender")]
        public GenderType Gender { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("lastLoginDate")]
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}
