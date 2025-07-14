using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers
{
    public class FeedController : Controller
    {
        [Authorize]
        public IActionResult MainFeed()
        {
            return View();
        }

        //[Authorize("Admin")]
        //public IActionResult Posts()
        //{
        //    return View();
        //}
    }
}
