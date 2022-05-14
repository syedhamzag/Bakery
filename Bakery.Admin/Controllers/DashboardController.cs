using Bakery.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IFileUpload fileUpload) : base(fileUpload)
        {
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Name") == null || HttpContext.Session.GetString("Name") == "") return RedirectToAction("Login","Account");
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            return View();
        }
    }
}
