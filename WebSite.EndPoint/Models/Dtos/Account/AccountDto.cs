using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.Dtos.Account
{
    public class AccountDto
    {
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }
        [Display(Name = "پست الکترونیک")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "به یاد داشتن")]
        public bool IsPersistent { get; set; } = false;

        public string ReturnUrl { get; set; } = "/";
    }
}
