using Microsoft.AspNetCore.Mvc;

namespace MedShop.Areas.Admin.Controllers
{
    [Route("/Admin/Admin/Index")]
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
