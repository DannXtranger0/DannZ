using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly MyDbContext _context;
        private readonly string targetType = "Post";
        public PostsApiController(MyDbContext context, Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] CreatePostDTO model)
        {
            var post = new Post
            {
                UserId = int.Parse(HttpContext.User.FindFirstValue("userId")!.ToString()),
                TextContent = model.TextContent ?? "",
                UploadedDateTime = DateTime.UtcNow,
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            if (model.MultimediaList != null)
            {
                foreach (var file in model.MultimediaList)
                {
                    if (file.FileName.Contains(".mp4"))
                    {
                        var uploadParams = new VideoUploadParams
                        {
                            File = new FileDescription(file.FileName, file.OpenReadStream()),
                            AssetFolder = "Posts"
                        };

                        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                        var mediaContentPost = new MediaContentPosts
                        {
                            PostId = post.Id,
                            MediaUrl = uploadResult.SecureUrl.ToString(),
                            MediaPublicId = uploadResult.PublicId.ToString(),
                        };
                        _context.MediaContentPost.Add(mediaContentPost);

                    }
                    else
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, file.OpenReadStream()),
                            AssetFolder = "Posts"
                        };

                        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                        var mediaContentPost = new MediaContentPosts
                        {
                            PostId = post.Id,
                            MediaUrl = uploadResult.SecureUrl.ToString(),
                            MediaPublicId = uploadResult.PublicId.ToString(),
                        };

                        _context.MediaContentPost.Add(mediaContentPost);
                    }
                }
                        
                await _context.SaveChangesAsync();

            }
            return Ok(new { message = "Post uploaded Succesfully!" });
        }

        [HttpGet]
        public async Task<ActionResult> FeedPost()
        {
            var posts = _context.Posts
                .Include(x => x.MediaContents)
                .Include(x => x.User)
                .ThenInclude(x => x.UserProfileImages)
                .Select(p => new FeedPostDTO
                {
                    UserAvatarUrl = p.User.UserProfileImages.AvatarUrl,
                    UserName = p.User.Name,
                    TextContent = p.TextContent,
                    UploadedDateTime = p.UploadedDateTime,
                    MultimediaUrl = p.MediaContents.Select(x=>x.MediaUrl).ToList()
                });

            return Ok(posts);
        }
    
    } 

}
