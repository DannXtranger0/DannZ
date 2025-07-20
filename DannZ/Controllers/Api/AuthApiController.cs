using DannZ.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Security.Claims;
using DannZ.Context;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using DannZ.Models;
using CloudinaryDotNet.Actions;
using DannZ.Services;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private MyDbContext _context;
        private string cookieName;
        private readonly IGetUserClaimsService _getUserClaims;
        public AuthApiController(MyDbContext context, IGetUserClaimsService getUserClaims, Cloudinary cloudinary, IConfiguration configuration)
        {
            _cloudinary = cloudinary;
            _getUserClaims = getUserClaims;
            _configuration = configuration;
            _context = context;
            cookieName = _configuration.GetValue<string>("CookieName")!;
        }
       
        [HttpPost]
        public async Task<ActionResult>Login([FromBody] LoginDTO login)
        {
            var account = _context.Users
                    .Include(x=>x.UserProfileImages)
                    .FirstOrDefault(x => x.Email == login.Email);

            bool matchPassword = BCrypt.Net.BCrypt.Verify(login.Password, account.Password);

            if (account == null || !matchPassword)
                    return BadRequest(new { error = "Incorrect Email Or Password" });

            //traigo los claims d el usuario
            var claimList = await _getUserClaims.GetClaims(account.RoleId);
                
            if(account?.UserProfileImages?.AvatarUrl!=null)
            {
                claimList.Add(new Claim("avatarUrl", account.UserProfileImages.AvatarUrl!));
            }

            claimList.Add(new Claim("userId", account!.Id.ToString()));

            var identity = new ClaimsIdentity(claimList, cookieName);

            //Propiedades de la cookie
            var props = new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            //crear principal 
            var principal = new ClaimsPrincipal(identity);


            //autenticar
            await HttpContext.SignInAsync(cookieName, principal, props);

            return Ok(new {message="Login Succesfully"});
        }
        

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] RegisterDTO model)
        {
            //Hashing  the password
            string password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            User user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = password,
                RoleId = 2

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //obtener las claims del usuario
            var claimList = await _getUserClaims.GetClaims(user.RoleId);

            if (model.Avatar != null)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model.Avatar.FileName, model.Avatar.OpenReadStream()),
                    AssetFolder = "Avatars"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //saving the avatar of the user in the corresponding table with neccesary data
                UserProfileImages uProfileImage = new UserProfileImages
                {
                    UserId = user.Id,
                    AvatarUrl = uploadResult.SecureUrl.ToString(),
                    AvatarPublicId = uploadResult.PublicId
                };

                _context.UserProfileImages.Add(uProfileImage);
                await _context.SaveChangesAsync();
                
                //Añado la claim de la url del avatar usuario para las vistas
                //en caso de que haya creado
                claimList.Add(new Claim("avatarUrl", uProfileImage.AvatarUrl));
            }
            
            //Añado la claim del userId para identificarlo en la vista
            claimList.Add(new Claim("userId", user.Id.ToString()));
        

            var identity = new ClaimsIdentity(claimList, cookieName);

            //crear principal 
            var principal = new ClaimsPrincipal(identity);

            //signOut
            await HttpContext.SignOutAsync();
            //signIn
            await HttpContext.SignInAsync(cookieName, principal);

            return Ok(new { message = "Account Created Succesfully" });

        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { message = "Logout Succesfully" });
        }
       
    }
}
