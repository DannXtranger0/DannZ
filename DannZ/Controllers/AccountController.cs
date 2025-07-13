using DannZ.Context;
using DannZ.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml;

namespace DannZ.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        public AccountController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (account == null)
                return NotFound();

            return View(account);
        }

        [Authorize(Policy = "OwnsProfile")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var account = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (account == null)
                return NotFound();

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

                
            } 
            return View(model);

        }


    }
}
