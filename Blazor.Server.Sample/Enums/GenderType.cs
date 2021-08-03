using System.ComponentModel.DataAnnotations;

namespace Blazor.Server.Sample.Enums
{
    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
}
