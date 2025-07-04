using DannZ.Models;
using DannZ.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UsersDTO userRecieved)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Name = userRecieved.Name,
                    UserName= userRecieved.Email,
                    Email = userRecieved.Email,
                    Biography = userRecieved.Biography,
                    AvatarUrl = userRecieved.AvatarUrl,
                    CoverUrl = userRecieved.CoverUrl,
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, userRecieved.Password);

                if (result.Succeeded)
                    return RedirectToAction("MainFeed","Posts");
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userRecieved);  
        }
    }
}
