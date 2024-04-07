using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Infrastructure.IdentityConfigs;
using Infrastructure.PasswordValidator;

namespace Infrastructore.IdentityConfig
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration["ConnectionString:SqlServer"];
            services.AddEntityFrameworkSqlServer().AddDbContext<IdentityDataBaseContext>(option => option.UseSqlServer(connection));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataBaseContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<CustomIdentityError>()
                .AddPasswordValidator<PasswordValidator>();

            //services.AddTransient<IClaimsTransformation, AddClaim>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            var roleManager = services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();
            IdentityRoleInitializer.CreateRoles(roleManager).Wait();

            return services;
        }
    }
    public class IdentityRoleInitializer
    {
        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Operator", "User", "Customer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
    //public class AddClaim : IClaimsTransformation
    //{
    //    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    //    {
    //        if(principal != null)
    //        {
    //            var identity =  principal.Identity as ClaimsIdentity;
    //            if(identity != null)
    //            {
    //                identity.AddClaim(new Claim("Role", "Admin"));
    //            }
    //        }
    //        return Task.FromResult(principal);
    //    }
    //}
}
