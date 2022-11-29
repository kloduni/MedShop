using MedShop.Core.Contracts;
using MedShop.Core.Services;
using MedShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedShop.Core.Cart;
using MedShop.Core.Models.ShoppingCart;
using MedShop.Extensions;
using MedShop.Core.Constants;
using MedShop.Models;

namespace MedShop.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly ShoppingCart shoppingCart;

        public OrderController(IProductService _productService, ShoppingCart _shoppingCart, IOrderService _orderService)
        {
            productService = _productService;
            shoppingCart = _shoppingCart;
            orderService = _orderService;
        }

        public async Task<IActionResult> All()
        {
            string userId = User.Id();

            var orders = await orderService.GetOrdersByUserIdAsync(userId);

            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }


        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product != null)
            {
                shoppingCart.AddItemToCart(product);
            }


            TempData[MessageConstant.SuccessMessage] = "Added to cart!";

            return RedirectToAction("All", "Product");
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var cartItem = await orderService.GetCartItemByIdAsync(id);

            if (cartItem != null)
            {
                shoppingCart.RemoveItemFromCart(cartItem);
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
