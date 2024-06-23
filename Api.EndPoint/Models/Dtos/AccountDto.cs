using System.ComponentModel.DataAnnotations;

namespace Api.EndPoint.Models.Dtos
{
    public class AccountDto
    {
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public bool IsPersistent { get; set; } = false;
    }
}


