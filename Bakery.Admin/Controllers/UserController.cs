using Bakery.DataTransferObject.DTOs.Users;
using Microsoft.AspNetCore.Mvc;
using Bakery.Admin.Models;
using Bakery.Admin.Services;
using Bakery.Services.Services;
using System.Threading.Tasks;

namespace Bakery.Admin.Controllers
{
    public class UserController : BaseController
    {
        private IUsersServices _userService;

        public UserController(IUsersServices userService, IFileUpload fileUpload) : base(fileUpload)
        {
            _userService = userService;
        }
        

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Reports()
        {
            if (HttpContext.Session.GetString("Name") == null || HttpContext.Session.GetString("Name") == "") return RedirectToAction("Login", "Account");
            var reports = await _userService.Get();
            return View(reports);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PostUser model)
        {
            var post = new PostUser();
            post.Name = model.Name;
            post.Email = model.Email;   
            post.RoleId = model.RoleId;
            post.Password = EncryptDecryptManager.Encrypt(model.Password!);
            post.IsActive = model.IsActive;
            post.Cell = model.Cell;

            var result = await _userService.Post(post);
            var content = await result.Content.ReadAsStringAsync();

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Edit(int userId)
        {
            var user = await _userService.Get(userId);
            var putEntity = new PutUser
            {
                Cell = user.Cell,
                Deleted = user.Deleted,
                Email = user.Email,
                IsActive = user.IsActive,
                Name = user.Name,
                Password = user.Password,
                RoleId = user.RoleId,
                UserId = user.UserId
            };
            return View(putEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PutUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PutUser put = new PutUser();
            put.Cell = model.Cell;
            put.Deleted = model.Deleted;
            put.Email = model.Email;
            put.IsActive = model.IsActive;
            put.Name = model.Name;
            put.Password = EncryptDecryptManager.Encrypt(model.Password);
            put.RoleId = model.RoleId;
            put.UserId = model.UserId;

            var entity = await _userService.Put(put);
            SetOperationStatus(new Models.OperationStatus
            {
                IsSuccessful = entity.IsSuccessStatusCode,
                Message = entity.IsSuccessStatusCode ? "Successful" : "Operation failed"
            });
            if (entity.IsSuccessStatusCode)

            {
                return RedirectToAction("Reports");
            }
            return View(model);

        }

    }
}
