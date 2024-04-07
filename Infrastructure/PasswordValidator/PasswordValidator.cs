using Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PasswordValidator
{
    internal class PasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            string[] CommonPasswords = File.ReadAllLines("CommonPasswords.txt");
            if (CommonPasswords.Contains(password))
            {
                var result = IdentityResult.Failed(new IdentityError
                {
                    Code = "CommonPasswords",
                    Description = "رمز عبور شما قابل شناسایی توسط ربات های هکر هست لطفا رمز عبور دیگری انتخاب کنید!"
                });
                return Task.FromResult(result);
            }
            return Task.FromResult(IdentityResult.Success);
            throw new NotImplementedException();
        }
    }
}
