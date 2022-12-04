using System.Globalization;
using MedShop.Core.Contracts;
using MedShop.Core.Models.Order;
using Microsoft.AspNetCore.Mvc;
using MedShop.Extensions;
using MedShop.Models;

namespace MedShop.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

        public async Task<IActionResult> All()
        {
            string userId = User.Id();
            var model = await orderService.GetOrdersModelByUserIdAsync(userId);

            return View(model);
        }
    }
}
