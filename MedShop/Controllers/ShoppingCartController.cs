using MedShop.Core.Constants;
using MedShop.Core.Models.ShoppingCart;
using MedShop.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedShop.Core.Contracts;
using MedShop.Core.Cart;

namespace MedShop.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly ShoppingCart shoppingCart;
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public ShoppingCartController(ShoppingCart _shoppingCart, IOrderService _orderService, IProductService _productService)
        {
            shoppingCart = _shoppingCart;
            orderService = _orderService;
            productService = _productService;
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();

            if (items.Count == 0)
            {
                TempData[MessageConstant.WarningMessage] = "Cart is empty!";

                return RedirectToAction("All", "Product");
            }
            shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = await shoppingCart.GetShoppingCartTotalAsync()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToShoppingCartAsync(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                TempData[MessageConstant.ErrorMessage] = "Product does not exist!";

                return RedirectToAction("All", "Product");
            }

            if (product.UsersProducts.Any(up => up.UserId == User.Id()))
            {
                TempData[MessageConstant.WarningMessage] = "You own this product!";

                return RedirectToAction("All", "Product");
            }

            if (product.Quantity <= 0)
            {
                TempData[MessageConstant.ErrorMessage] = "No quantity available!";

                return RedirectToAction("All", "Product");
            }


            await shoppingCart.AddItemToCartAsync(product);



            TempData[MessageConstant.SuccessMessage] = "Added to cart!";

            return RedirectToAction("All", "Product");
        }

        public async Task<IActionResult> RemoveItemFromShoppingCartAsync(int id)
        {
            var cartItem = await orderService.GetCartItemByIdAsync(id);

            if (cartItem != null)
            {
                await shoppingCart.RemoveItemFromCartAsync(cartItem);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = User.Id();
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
