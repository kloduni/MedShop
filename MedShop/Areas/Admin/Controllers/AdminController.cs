using Microsoft.AspNetCore.Mvc;

namespace MedShop.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
