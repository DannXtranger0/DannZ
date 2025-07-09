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

        [Authorize("Admin")]
        public IActionResult Posts()
        {
            return View();
        }
    }
}
