using MedShop.Core.Constants;
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

        public ProductController(IProductService _productService)
        {
            productService = _productService;
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

            var model = new ProductBaseModel()
            {
                ProductCategories = await productService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductBaseModel model)
        {

            if (await productService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.ProductCategories = await productService.AllCategoriesAsync();
                return View(model);
            }

            var userId = User.Id();

            int id = await productService.CreateAsync(model, userId);

            TempData[MessageConstant.SuccessMessage] = "Product added successfully!";

            return RedirectToAction(nameof(Details), new {id});
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

        public async Task<IActionResult> MyProducts()
        {
            IEnumerable<ProductServiceModel> myProducts;
            var userId = User.Id();

            myProducts = await productService.AllProductsByUserIdAsync(userId);

            return View(myProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await productService.ExistsAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not exist!";

                return RedirectToAction(nameof(All));
            }

            if ((await productService.HasUserWithIdAsync(id, User.Id())) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not belong to this user!";

                return RedirectToAction(nameof(All));
            }

            var product = await productService.ProductDetailsByIdAsync(id);
            var categoryId = await productService.GetProductCategoryIdAsync(id);

            var model = new ProductBaseModel()
            {
                Id = id,
                CategoryId = categoryId,
                ProductName = product.ProductName,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = product.Quantity,
                ProductCategories = await productService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductBaseModel model)
        {
            if ((await productService.ExistsAsync(model.Id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not exist!";
                model.ProductCategories = await productService.AllCategoriesAsync();

                return View(model);
            }

            if ((await productService.HasUserWithIdAsync(model.Id, User.Id())) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not belong to this user!";

                return RedirectToAction(nameof(All));
            }

            if ((await productService.CategoryExistsAsync(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
                model.ProductCategories = await productService.AllCategoriesAsync();

                return View(model);
            }

            if (ModelState.IsValid == false)
            {
                model.ProductCategories = await productService.AllCategoriesAsync();

                return View(model);
            }

            await productService.EditAsync(model.Id, model);

            TempData[MessageConstant.SuccessMessage] = "Success!";

            return RedirectToAction(nameof(Details), new {model.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await productService.ExistsAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not exist!";

                return RedirectToAction(nameof(All));
            }

            if ((await productService.HasUserWithIdAsync(id, User.Id())) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not belong to this user!";

                return RedirectToAction(nameof(All));
            }

            var product = await productService.ProductDetailsByIdAsync(id);
            var model = new ProductServiceModel()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity= product.Quantity,
                Seller = product.Seller
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductServiceModel model)
        {
            if ((await productService.ExistsAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not exist!";

                return RedirectToAction(nameof(All));
            }

            if ((await productService.HasUserWithIdAsync(id, User.Id())) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not belong to this user!";

                return RedirectToAction(nameof(All));
            }

            await productService.DeleteAsync(id);

            TempData[MessageConstant.SuccessMessage] = "Product deleted successfully!";

            return RedirectToAction(nameof(All));
        }
    }
}
