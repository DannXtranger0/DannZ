using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DannZ.Context;
using DannZ.Models;
using DannZ.Models.DTO;
using DannZ.Models.DTO.Account;
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
    }
}
