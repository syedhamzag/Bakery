using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Bakery.DataTransferObject.DTOs.Users;
using Bakery.Admin.Models;
using Bakery.Admin.Services;
using Bakery.Services.Services;
using System.Security.Claims;

namespace Bakery.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private IUsersServices _userService;

        public AccountController(IUsersServices userService, IFileUpload fileUpload) : base(fileUpload)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(PostUser model)
        {
            string? pass = EncryptDecryptManager.Encrypt(model.Password!);
            var users = await _userService.Get();
            var user = users.Find(s => s.Email == model.Email && s.Password == pass && s.IsActive == true);
            if (user == null) return RedirectToAction("Login");
            {
                HttpContext.Session.SetString("Name", user.Name!);
                return RedirectToAction("Index", "Dashboard");
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Upload()
        {
            return View();
        }
    }
}
