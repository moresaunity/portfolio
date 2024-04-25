using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class ChatRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
