using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult MainFeed()
        {
            return View();
        }
    }
}
