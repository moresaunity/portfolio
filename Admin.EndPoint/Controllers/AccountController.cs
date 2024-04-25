﻿using Admin.EndPoint.Models.Dtos;
using Admin.EndPoint.Models.Dtos.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Security.Claims;

namespace Admin.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountDto accountDto)
        {
            var client = new RestClient("https://localhost:7239");
            var request = new RestRequest("/api/v1/Account", Method.Post);
            request.AddJsonBody(accountDto);
            AccountResultDto? result = client.Post<AccountResultDto>(request);
            if (!result.IsSuccess) return View();
                
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Model.FullName ?? "پشتیبان سایت"),
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
            return SignIn(new ClaimsPrincipal(identity), propertise, CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
