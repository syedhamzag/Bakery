using Microsoft.AspNetCore.Mvc;
using Bakery.Admin.Models;
using Bakery.Services.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Bakery.Admin.Controllers
{
    public class BaseController : Controller
    {
        public const string cacheKey = "baserecord_key";
        public readonly IFileUpload _fileUpload;
        public BaseController(IFileUpload fileUpload)
        {
            _fileUpload = fileUpload;
        }


        public void SetOperationStatus(OperationStatus operationStatus)
        {
            TempData["OperationStatus"] = JsonConvert.SerializeObject(operationStatus);

        }


        public async Task<IActionResult> DownloadFile(string file)
        {
            var filestream = await _fileUpload.DownloadFile(file);
            filestream.Item1.Position = 0;
            return File(filestream.Item1, filestream.Item2);
        }
    }
}
