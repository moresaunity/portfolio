using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.Dtos.Account
{
    public class ReturnLoginViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد نمایید.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        [Required]
        [Display(Name = "پست الکترونیک")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public List<Links> Links { get; set; }
    }
}
