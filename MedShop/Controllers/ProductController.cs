using MedShop.Core.Contracts;
using MedShop.Core.Models.Product;
using MedShop.Extensions;
using MedShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedShop.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ITraderService traderService;

        public ProductController(IProductService _productService, ITraderService _traderService)
        {
            productService = _productService;
            traderService = _traderService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllProductsQueryModel query)
        {
            var result = await productService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            query.TotalProductsCount = result.TotalProductsCount;
            query.Categories = await productService.AllCategoriesNamesAsync();
            query.Products = result.Products;

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await traderService.ExistsByIdAsync(User.Id()) == false)
            {
                return RedirectToAction("Become", "Trader");
            }

            var model = new ProductBaseModel()
            {
                ProductCategories = await productService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductBaseModel model)
        {
            if (await traderService.ExistsByIdAsync(User.Id()) == false)
            {
                return RedirectToAction("Become", "Trader");
            }

            if (await productService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.ProductCategories = await productService.AllCategoriesAsync();
                return View(model);
            }

            int traderId = await traderService.GetTraderIdAsync(User.Id());

            int id = await productService.CreateAsync(model, traderId);

            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await productService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await productService.ProductDetailsByIdAsync(id);

            return View(model);
        }
    }
}
