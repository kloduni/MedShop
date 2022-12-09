using MedShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MedShop.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using static MedShop.Areas.Admin.AdminConstants;

namespace MedShop.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService productService;

        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = AreaName });
            }

            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Product");
            }

            var model = await productService.AllCarousel();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}