using DannZ.Models;
using DannZ.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata;
using DannZ.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace DannZ.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login() => View();
        public IActionResult Create() => View();
        public IActionResult Forbidden() => View();

    }
}
