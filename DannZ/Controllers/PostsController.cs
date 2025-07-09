using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers
{
    public class PostsController : Controller
    {
        [Authorize]
        public IActionResult MainFeed()
        {
            return View();
        }
        public IActionResult Posts()
        {
            return View();
        }
    }
}
