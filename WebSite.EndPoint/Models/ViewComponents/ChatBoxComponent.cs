using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Models.ViewComponents
{
    public class ChatBoxComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(viewName: "ChatBoxComponent");
        }
    }
}
