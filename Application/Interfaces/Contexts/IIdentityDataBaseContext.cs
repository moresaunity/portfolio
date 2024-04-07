using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts
{
    public interface IIdentityDataBaseContext
    {
        DbSet<User> Users { get; set; }
    }
}
