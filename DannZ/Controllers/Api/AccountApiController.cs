using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO;
using DannZ.Models.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private AuthValidations _validations;
        private MyDbContext _context;
        private string cookieName; 
        public AccountApiController(MyDbContext context, Cloudinary cloudinary, IConfiguration configuration)
        {
            _cloudinary = cloudinary;
            _configuration = configuration;
            _context = context;
            cookieName = _configuration.GetValue<string>("CookieName")!;
            _validations = new AuthValidations(_context);
        }

        [HttpGet("Profile/{id}")]
        public async Task<ActionResult<User>> Profile(int? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var account = await _context.Users
                    .Include(x => x.UserProfileImages)
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if (account == null)
                    return NotFound();

                var userData = new ProfileDTO
                {
                    Id = account.Id,
                    Name = account.Name,
                    Email = account.Email,
                    AvatarUrl = account.UserProfileImages!.AvatarUrl,
                    CoverUrl= account.UserProfileImages.CoverUrl,
                    Biography = account.Biography
                };

                return Ok(userData);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

         
        }


        [HttpPost("Edit/{id}")]
        [Authorize(Policy = "OwnsProfile")]
        public async Task<IActionResult> Edit([FromForm] EditUserDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Fill The Fields Correctly");

            var user = await _context.Users
                .Include(x=>x.UserProfileImages)
                .FirstOrDefaultAsync(u => u.Id == model.Id);
            
            if (user == null)
                return BadRequest("This User Doesn't Exists");

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!passwordMatch)
                return BadRequest("The Passwort Doesn't Match");

            user.Name = model.Name;
            user.Email = model.Email;

            if (model.AvatarUrl != null)
            {
                await UploadImage("Avatar", user, model);
                await _context.SaveChangesAsync();
            }
            
            if (model.CoverUrl != null)
            {
                await UploadImage("Cover", user, model);
                await _context.SaveChangesAsync();
            }
        
            await _context.SaveChangesAsync();

            return Ok(new { message = "Account Updated Succesfully!" });

        }

        public async Task UploadImage(string imageType, User user, EditUserDTO model)
        {
            if (imageType == "Avatar")
            {
                //Create the new Avatar
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model?.AvatarUrl!.FileName, model.AvatarUrl!.OpenReadStream()),
                    AssetFolder = "Avatars"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //Delete the old Avatar
                if (user.UserProfileImages.AvatarUrl != null)
                {
                    var deleteParams = new DeletionParams(user.UserProfileImages.AvatarPublicId);
                    var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                    user.UserProfileImages.AvatarUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.AvatarPublicId = uploadResult.PublicId.ToString();
                }
                else if (user.UserProfileImages.CoverUrl != null && user.UserProfileImages.AvatarUrl== null)
                {
                    //Assign the new Avatar to the user
                    user.UserProfileImages.CoverUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId = uploadResult.PublicId.ToString();
                }
                else
                {
                    //Assign the new Avatar to the user
                    var uProfileImages = new UserProfileImages
                    {
                        UserId = user.Id,
                        AvatarUrl = uploadResult.SecureUrl.ToString(),
                        AvatarPublicId = uploadResult.PublicId.ToString(),
                    };
                    _context.UserProfileImages.Add(uProfileImages);
                }
            }
            else if (imageType == "Cover")
            {
                //Create the new Avatar
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(model?.CoverUrl!.FileName, model?.CoverUrl!.OpenReadStream()),
                    AssetFolder = "Covers"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                //Delete the old Avatar
                if (user.UserProfileImages?.CoverUrl!= null)
                {
                    var deleteParams = new DeletionParams(user.UserProfileImages.CoverPublicId);
                    var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                    user.UserProfileImages.CoverUrl= uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId= uploadResult.PublicId.ToString();
                }
                else if(user.UserProfileImages.AvatarUrl !=null && user.UserProfileImages.CoverUrl==null)
                {
                    //Assign the new Avatar to the user
                    user.UserProfileImages.CoverUrl = uploadResult.SecureUrl.ToString();
                    user.UserProfileImages.CoverPublicId = uploadResult.PublicId.ToString();
                }
                else
                {
                    var uProfileImages = new UserProfileImages
                    {
                        UserId = user.Id,
                        CoverUrl = uploadResult.SecureUrl.ToString(),
                        CoverPublicId= uploadResult.PublicId.ToString(),
                    };
                    _context.UserProfileImages.Add(uProfileImages);

                }
            }
        }
    }
}
