using DannZ.Context;
using DannZ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DannZ.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;
        public AccountController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("Profile/{id}")]
        public async Task<ActionResult<User>> Profile(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }
    }
}
