using Api.EndPoint.Models.Dtos;
using Appliances.Common;
using AutoMapper;
using Domain.Dtos;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.EndPoint.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        private readonly string CacheKey = "Users";
        public UserController(UserManager<User> userManager, IMapper mapper, IMemoryCache cache, ILogger<UserController> logger)
        {
            _userManager = userManager;
            this.mapper = mapper;
            this.cache = cache;
            this.logger = logger;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            logger.LogInformation("This Is a Error");
            List<ReturnLoginViewModel> userModels = new List<ReturnLoginViewModel>();
            if (!cache.TryGetValue(CacheKey, out userModels))
            {
                userModels = new List<ReturnLoginViewModel>();

                List<User> users = await _userManager.Users.ToListAsync();
                foreach (var item in users)
                {
                    List<string> roles = (await _userManager.GetRolesAsync(item)).ToList();
                    ReturnLoginViewModel user = mapper.Map<ReturnLoginViewModel>(item);
                    user.Links = GenerateLinks(item.Id);
                    user.Roles = roles;

                    userModels.Add(user);
                }

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .RegisterPostEvictionCallback(CacheCallBack, this)
                    .SetSize(1);

                cache.Set(CacheKey, userModels, cacheOptions);
            }
            else userModels = (List<ReturnLoginViewModel>)cache.Get(CacheKey);

            return Ok(new BaseDto<List<ReturnLoginViewModel>>(true, new List<string> { "Get Users Is Success" }, userModels));
        }

        /// <summary>
        /// Find User By Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> Get(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new BaseDto(false, new List<string> { "User Not Found" }));
            List<string> roles = (await _userManager.GetRolesAsync(user)).ToList();
            ReturnLoginViewModel result = mapper.Map<ReturnLoginViewModel>(user);
            result.Links = GenerateLinks(result.Id);
            result.Roles = roles;

            return Ok(new BaseDto<ReturnLoginViewModel>(true, new List<string> { "Get User By Id Is Success" }, result));
        }

        /// <summary>
        /// Log Out User
        /// </summary>
        /// <param name="Authorization">Token</param>
        /// <returns>Status</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromHeader] string Authorization)
        {

            string Token = Authorization.Replace("Bearer ", "");
            SecurityHasher hasher = new SecurityHasher();
            User? user = await _userManager.FindByLoginAsync("none", hasher.HashPassword(Token));

            if (user == null)
                return NotFound(new BaseDto(false, new List<string> { "User Not Found!" }));

            await _userManager.RemoveAuthenticationTokenAsync(user, "none", "jwtToken");

            cache.Remove(CacheKey);
            return Ok(new BaseDto(true, new List<string> { "Log Out is Success" }));
        }

        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="model">User information</param>
        /// <returns>Message</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> Put(string id, [FromBody] AccountDto model)
        {
            List<string> errors = new List<string>();
            User? user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new BaseDto(false, new List<string> { "User Not Found!" }));

            // Update user properties
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                cache.Remove(CacheKey);
                return Ok(new BaseDto<ReturnLoginViewModel>(true, new List<string> { "Update User Is Success" }, mapper.Map<ReturnLoginViewModel>(user)));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }
                return BadRequest(new BaseDto(false, errors));
            }
        }

        // Keep this :
        // mapper.Map<ReturnLoginViewModel>(user)

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Message</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Operator")]
        public async Task<IActionResult> Delete(string id)
        {
            List<string> errors = new List<string>();
            User? user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new BaseDto(false, new List<string> { "User Not Found!" }));

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                cache.Remove(CacheKey);
                return Ok(new BaseDto(true, new List<string> { "Delete User Is Success" }));
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }

                return BadRequest(new BaseDto(false, errors));
            }
        }
        private List<Links> GenerateLinks(string Id)
        {
            return new List<Links>
                {
                    new Links
                    {
                        Href = Url.Action(nameof(Get), "User", new { Id }, Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Delete), "User", new { Id }, Request.Scheme),
                        Rel = "Delete",
                        Method = "Delete"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Put), "user", new { Id }, Request.Scheme),
                        Rel = "Update",
                        Method = "Put"
                    }
                };

        }
        private static void CacheCallBack(object Key, object value, EvictionReason reason, object state)
        {

        }
    }
}
