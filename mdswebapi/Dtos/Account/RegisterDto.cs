using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace mdswebapi.Dtos.Account
{
    public class RegisterDto
    {
        [Microsoft.Build.Framework.Required]
        public string? CustomerName { get; set; }
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        public string? CustomerEmail { get; set; }
        [Microsoft.Build.Framework.Required]
        public string? CustomerPassword { get; set; }
    }
}
