using Api.EndPoint.Models.Dtos;
using Appliances.Common;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.EndPoint.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly IConfiguration configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, IMemoryCache cache)
        {
            this.configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            this.cache = cache;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AccountDto model)
        {
            User user = await _userManager.FindByNameAsync(model.PhoneNumber);
            string status = "";

            if (user == null)
            {
                User newUser = new User()
                {
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.PhoneNumber,
                    Email = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
                if (!result.Succeeded)
                {
                    string message = "";
                    foreach (var error in result.Errors)
                    {
                        message += error.Description + Environment.NewLine;
                    }
                    return Ok(new AccountResultDto { IsSuccess = false, Message = message });
                }

                IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, "User");
                if (!roleResult.Succeeded)
                {
                    string message = "";
                    foreach (var error in roleResult.Errors)
                    {
                        message += error.Description + Environment.NewLine;
                    }
                    return Ok(new AccountResultDto { IsSuccess = false, Message = message });
                }

                user = await _userManager.FindByNameAsync(model.PhoneNumber);
                status = "Register";
            }
            else
            {
                await _signInManager.SignOutAsync();
                user = await _userManager.FindByNameAsync(model.PhoneNumber);
                if (!user.IsActive)
                {
                    return Ok(new AccountResultDto { IsSuccess = false, Message = "شما غیر فعال هستید و اجازه دسترسی ندارید." });
                }
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                if (!signInResult.Succeeded)
                {
                    if (signInResult.RequiresTwoFactor)
                    {
                        return Ok(new AccountResultDto { IsSuccess = false, Message = "RequiresTwoFactor" });
                    }
                    else if (signInResult.IsLockedOut)
                    {
                        return Ok(new AccountResultDto { IsSuccess = false, Message = "شما به دلیل درخواست بیش از حد قفل شده این یک بار مرورگر را بسته و دوباره باز کنید." });
                    }
                    return Ok(new AccountResultDto { IsSuccess = false, Message = "رمز عبور وارد شده صحیح نمیباشد." });
                }
                status = "Login";
            }

            string jwtToken = await CreateJwtToken(user, status);
            if (jwtToken == "false")
            {
                return Ok(new AccountResultDto { IsSuccess = false, Message = "توکن صحیح نیست" });
            }

            List<string> Roles = new List<string>();
            foreach (var item in _userManager.GetRolesAsync(user).Result)
            {
                Roles.Add(item);
            }

            ReturnLoginViewModel viewModel = new ReturnLoginViewModel()
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Roles = Roles,
                Links = new List<Links>
                {
                    new Links
                    {
                        Href = Url.Action("Get", "User", new { user.Id }, Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    },
                    new Links
                    {
                        Href = Url.Action("Delete", "User", new { user.Id }, Request.Scheme),
                        Rel = "Delete",
                        Method = "Delete"
                    },
                    new Links
                    {
                        Href = Url.Action("Put", "User", new { user.Id }, Request.Scheme),
                        Rel = "Update",
                        Method = "Put"
                    }
                }
            };



            string url = Url.Action("Get", "User", new { user.Id }, Request.Scheme);

            if (status == "Register")
            {
                cache.Remove("Users");
                return Created(url, new AccountResultDto { IsSuccess = true, Message = "ثبت نام شما با موفقیت انجام شد.", Token = jwtToken, Model = viewModel, Url = url });
            }

            return Ok(new AccountResultDto { IsSuccess = true, Message = "ورود شما با موفقیت انجام شد.", Token = jwtToken, Model = viewModel, Url = url });
        }
        /// <summary>
        /// Create Jwt Token For Users
        /// </summary>
        /// <param name="user">User information</param>
        /// <param name="status">Status</param>
        /// <returns></returns>
        private async Task<string> CreateJwtToken(User user, string status)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, status, "jwtToken");

            var role = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id),
                new Claim("Email", user.Email),
            };
            foreach (var claim in role)
            {
                claims.Add(new Claim(ClaimTypes.Role, claim ?? "User"));
            }

            string key = configuration["JWtConfig:key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            DateTime tokenExp = DateTime.Now.AddDays(int.Parse(configuration["JWtConfig:expires"]));
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWtConfig:issuer"],
                audience: configuration["JWtConfig:audience"],
                expires: tokenExp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            SecurityHasher hasher = new SecurityHasher();
            IdentityResult tokenAdded = await _userManager.SetAuthenticationTokenAsync(user, "none", "jwtToken", hasher.HashPassword(jwtToken));
            if (!tokenAdded.Succeeded)
            {
                return "false";
            }

            return jwtToken;
        }
    }
}
