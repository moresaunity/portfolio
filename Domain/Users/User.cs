using Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    [Auditable]
    public class User: IdentityUser
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
