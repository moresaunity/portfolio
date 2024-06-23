using Admin.EndPoint.Models.Dtos;
using Admin.EndPoint.Models.Dtos.Account;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Admin.EndPoint.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            RestClient client = new RestClient("http://localhost:5168");
            RestRequest request = new RestRequest("/api/v1/User", Method.Get);
            BaseDto<List<ReturnLoginViewModel>>? result = client.Get<BaseDto<List<ReturnLoginViewModel>>>(request);

            return View(result?.Data);
        }
        [HttpGet("id")]
        public IActionResult Index(string id)
        {
            RestClient client = new RestClient("http://localhost:5168");
            RestRequest request = new RestRequest("/api/v1/User/" + id, Method.Get);
            BaseDto<ReturnLoginViewModel>? result = client.Get<BaseDto<ReturnLoginViewModel>>(request);

            return View(result?.Data);
        }
        //[HttpGet]
        //public IActionResult Edit(int id, AccountDto model)
        //{
        //    RestClient client = new RestClient("http://localhost:5168");
        //    RestRequest request = new RestRequest("/api/v1/User/" + id, Method.Get);
        //    BaseDto<ReturnLoginViewModel>? result = client.Get<BaseDto<ReturnLoginViewModel>>(request);

        //}
    }
}
