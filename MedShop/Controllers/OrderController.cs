using MedShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using MedShop.Extensions;

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
            var model = await orderService.GetOrdersByUserIdAsync(userId);

            return View(model);
        }
    }
}
