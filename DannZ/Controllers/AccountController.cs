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
        [HttpGet]
        public IActionResult Profile(int? id) => View(id);


        [Authorize(Policy = "OwnsProfile")]
        [HttpGet]
        public  IActionResult Edit(int? id )  => View();


        //[HttpPost]
        //public async Task<IActionResult> Edit(RegisterDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);


        //    } 
        //    return View(model);

        //}


    }
}
