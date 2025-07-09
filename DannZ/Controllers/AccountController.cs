using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
