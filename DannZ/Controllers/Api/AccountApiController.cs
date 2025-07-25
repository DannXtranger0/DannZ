﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO;
using DannZ.Models.DTO.Account;
using DannZ.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IUploadProfileImageService _uploadProfileImageService;
        private readonly IGetUserClaimsService _getUserClaims;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private MyDbContext _context;
        private string cookieName;
        public AccountApiController(MyDbContext context, IGetUserClaimsService getUserClaims, Cloudinary cloudinary, IConfiguration configuration, IUploadProfileImageService uploadProfileImageService)
        {
            _cloudinary = cloudinary;
            _configuration = configuration;
            _getUserClaims = getUserClaims;
            _context = context;
            cookieName = _configuration.GetValue<string>("CookieName")!;
            _uploadProfileImageService = uploadProfileImageService;
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
                    AvatarUrl = account.UserProfileImages?.AvatarUrl,
                    CoverUrl= account.UserProfileImages?.CoverUrl,
                    Biography = account.Biography
                };

                return Ok(userData);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

         
        }


        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "OwnsProfile")]
        public async Task<ActionResult> Edit([FromForm] EditUserDTO model)
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
            var claimList = await _getUserClaims.GetClaims(user.RoleId);

            if (model.AvatarUrl != null)
            {
                await _uploadProfileImageService.UploadImage("Avatar", user, model);
                await _context.SaveChangesAsync();
                claimList.Add(new Claim("avatarUrl", user.UserProfileImages!.AvatarUrl!));
            }

            if (model.CoverUrl != null)
            {
                await _uploadProfileImageService.UploadImage("Cover", user, model);
                await _context.SaveChangesAsync();
            }
        
            await _context.SaveChangesAsync();

            //Añado la claim del userId para identificarlo en la vista
            claimList.Add(new Claim("userId", user.Id.ToString()));

            var identity = new ClaimsIdentity(claimList, cookieName);

            //crear principal 
            var principal = new ClaimsPrincipal(identity);

            //signOut
            await HttpContext.SignOutAsync();
            //signIn
            await HttpContext.SignInAsync(cookieName, principal);

            return Ok(new { message = "Account Updated Succesfully!" });
        }

        [HttpPatch("Edit/Bio/{id}")]
        [Authorize(Policy = "OwnsProfile")]
        public async Task<ActionResult> EditBio(int id, [FromBody] string newBio)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound("This User Doesn't Exist");

            user.Biography = newBio;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Biography Updated Successfully" });
        }
    }
}
