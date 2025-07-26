using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO.Comments;
using DannZ.Models.DTO.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        public async Task<ActionResult> FeedPost([FromQuery]string? userId,[FromQuery] string? search)
        {
            var posts = _context.Posts
               .Include(p => p.Comment_Posts)
               .Include(p => p.MediaContents)
               .Include(p => p.User)
               .ThenInclude(x => x.UserProfileImages)
               .Select(p => new FeedPostDTO
               {
                   PostId = p.Id,
                   UserId = p.UserId,
                   UserAvatarUrl = p.User.UserProfileImages.AvatarUrl,
                   UserName = p.User.Name,
                   TextContent = p.TextContent,
                   UploadedDateTime = p.UploadedDateTime,
                   CommentsPost = p.Comment_Posts.Select(c=> new CommentPostDTO
                   {
                      commentId = c.Id,
                      PostId=c.PostId,
                      UserId=c.Id,
                      CommentContent =c.CommentContent,
                      UserName = c.User.Name,
                      UserAvatarUrl = c.User.UserProfileImages.AvatarUrl,
                      UploadedDateTime =c.UploadedDateTime,
                   }).ToList(),
                   MultimediaUrl = p.MediaContents.Select(x => x.MediaUrl).ToList(),
               });

            if (!string.IsNullOrEmpty(userId))
            {
                int userIdInt = int.Parse(userId);
                posts = posts.Where(x => x.UserId == userIdInt);
            }
           

            return Ok(posts);
        }
    
    } 

}
