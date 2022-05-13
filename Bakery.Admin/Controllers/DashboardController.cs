using AlMuslimeen.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlMuslimeen.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IFileUpload fileUpload) : base(fileUpload)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
