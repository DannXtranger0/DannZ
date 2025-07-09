using DannZ.Models;
using DannZ.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata;
using DannZ.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace DannZ.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private AuthValidations _validations;
        private MyDbContext _context;
        private string cookieName;
        public AuthController(MyDbContext context, Cloudinary cloudinary, IConfiguration configuration)
        {
            _cloudinary = cloudinary;
            _configuration = configuration;
            _context = context;
            cookieName = _configuration.GetValue<string>("CookieName")!;
            _validations = new AuthValidations(_context);
        }


        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async  Task<IActionResult> Login(LoginDTO login)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (!_validations.ValidateEmail(login.Email))
        //        {
        //            ViewData["EmailError"] = "This mail is not registered in the system";
        //            return View(login);
        //        }

        //        var account = _context.Users
        //            .FirstOrDefault(x=> x.Email== login.Email && x.Password== login.Password);

        //        if (account != null) 
        //        {
        //            //var permissions = _context.RolePermissions
        //            //Traigo los permisos y los convierto a claims

        //            //var identity = new ClaimsIdentity(account.claim)
        //        }

        //    }

        //}




        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RegisterDTO model, IFormFile? avatar)
        {
            if (ModelState.IsValid)
            {
                if (_validations.ValidateEmail(model.Email))
                {
                    ViewData["EmailExist"] = "This email is already registered";
                    return View(model);
                }

                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };

                if (avatar != null)
                {
                    var uploadParams = new ImageUploadParams
                    {

                        File = new FileDescription(avatar.FileName, avatar.OpenReadStream()),
                        AssetFolder = "Avatars"
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    user.AvatarUrl = uploadResult.SecureUrl.ToString();
                }

                //Assign permissions
                _context.Users.Add(user);

                user.RoleId = 2;

                await _context.SaveChangesAsync();

                //obtener las claims del usuario
                var userClaims = await _context.RolePermissions.Where(x => x.RoleId == user.RoleId).Include(x => x.Permissions).Select(x => x.Permissions).ToListAsync();
                List<Claim> claimList = new List<Claim>();

                foreach(var permission in userClaims)
                {
                    claimList.Add(new Claim("permission",permission!.ToString()!));
                }

                var identity = new ClaimsIdentity(claimList, cookieName);

                //crear principal 
                var principal = new ClaimsPrincipal(identity);


                //autenticar
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("MainFeed", "Posts");
            }
            return View(model);
        }


        //[AllowAnonymous]
        //public IActionResult Login(string ReturnUrl)
        //{
        //    LoginDTO login = new LoginDTO();
        //    login.ReturnUrl = ReturnUrl;
        //    return View(login);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginDTO loginRecieved)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AppUser appUser = await _userManager.FindByEmailAsync(loginRecieved.Email);

        //        if(appUser != null)
        //        {
        //            await _signInManager.SignOutAsync();
        //            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
        //                .PasswordSignInAsync(appUser, loginRecieved.Password, loginRecieved.RememberMe,false);

        //            if (result.Succeeded)
        //                return Redirect(loginRecieved.ReturnUrl ?? "/");
        //        }
        //    }
        //    return View(loginRecieved);

        //}
    }
}
