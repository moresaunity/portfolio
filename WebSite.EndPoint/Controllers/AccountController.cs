using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Security.Claims;
using WebSite.EndPoint.Models.Dtos.Account;

namespace WebSite.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return View(nameof(LoginOrRegister));
            return View();
        }
        public IActionResult LoginOrRegister()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginOrRegister(AccountDto accountDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var client = new RestClient("http://localhost:5168");
            var request = new RestRequest("/api/v1/Account", Method.Post);
            request.AddJsonBody(accountDto);
            AccountResultDto? result = client.Post<AccountResultDto>(request);
            if (!result.IsSuccess) return View();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Model.FullName ?? "کاربر"),
                new Claim(ClaimTypes.NameIdentifier, result.Model.Id),
                new Claim(ClaimTypes.MobilePhone, result.Model.PhoneNumber),
                new Claim(ClaimTypes.Email, result.Model.Email),
                new Claim("Token", result.Token)
            };
            foreach (var item in result.Model.Roles)
                claims.Add(new Claim(ClaimTypes.Role, item.ToString()));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var propertise = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.Now.AddDays(10),
                RedirectUri = Url.Content("/Home/Index")
            };
            _logger.LogInformation("Register a New User", result);
            return SignIn(new ClaimsPrincipal(identity), propertise, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
