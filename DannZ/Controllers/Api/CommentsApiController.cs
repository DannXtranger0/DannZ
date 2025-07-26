using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsApiController : ControllerBase
    {
        private readonly MyDbContext _context;
        public CommentsApiController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CreateCommentPostDTO modelComment)
        {
            var comment = new CommentPost
            {
                PostId = modelComment.PostId,
                UserId = modelComment.UserId,
                CommentContent = modelComment.CommentContent!,
                UploadedDateTime = DateTime.UtcNow,
            };

            _context.CommentPosts.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }
    }
}
