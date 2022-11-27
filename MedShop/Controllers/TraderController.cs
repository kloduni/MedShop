using MedShop.Core.Constants;
using MedShop.Core.Contracts;
using MedShop.Core.Models.Trader;
using MedShop.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MedShop.Controllers
{
    public class TraderController : BaseController
    {
        private readonly ITraderService traderService;

        public TraderController(ITraderService _traderService)
        {
            traderService = _traderService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await traderService.ExistsByIdAsync(User.Id()))
            {
                TempData[MessageConstant.ErrorMessage] = "You are already a Trader!";

                return RedirectToAction("Index", "Home");

            }
            var model = new BecomeTraderModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeTraderModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await traderService.ExistsByIdAsync(userId))
            {
                TempData[MessageConstant.ErrorMessage] = "You are already a Trader!";

                return View(model);
            }

            if (await traderService.UserWithTraderNameExists(model.TraderName))
            {
                TempData[MessageConstant.ErrorMessage] = "Trade name already in use!";

                return View(model);
            }

            if (await traderService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                TempData[MessageConstant.ErrorMessage] = "Phone number already in use!";

                return View(model);
            }

            await traderService.Create(userId, model.TraderName, model.PhoneNumber);

            return RedirectToAction("All", "Product");
        }
    }
}
