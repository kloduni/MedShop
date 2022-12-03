using MedShop.Core.Constants;
using MedShop.Core.Contracts.Admin;
using MedShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService _userService, UserManager<User> _userManager)
        {
            userService = _userService;
            userManager = _userManager;
        }
        public async Task<IActionResult> All()
        {
            var model = await userService.All();

            return View(model);
        }

        public async Task<IActionResult> Ban(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData[MessageConstant.ErrorMessage] = "User not found!";
                return RedirectToAction(nameof(All));

            }

            if (user.IsActive)
            {
                TempData[MessageConstant.SuccessMessage] = "User banned!";
                await userService.BanUserByIdAsync(user);
            }
            else
            {
                TempData[MessageConstant.WarningMessage] = "User already banned!";
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Unban(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData[MessageConstant.ErrorMessage] = "User not found!";
                return RedirectToAction(nameof(All));

            }

            if (user.IsActive)
            {
                TempData[MessageConstant.ErrorMessage] = "User is not banned!";
                
            }
            else
            {
                await userService.UnbanUserByIdAsync(user);
                TempData[MessageConstant.SuccessMessage] = "User unbanned!";
            }

            return RedirectToAction(nameof(All));
        }
    }
}
