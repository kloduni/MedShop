﻿using MedShop.Core.Constants;
using MedShop.Core.Models.User;
using MedShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace MedShop.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IMemoryCache cache;

        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager, RoleManager<IdentityRole> _roleManager, IMemoryCache _cache)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
            cache = _cache;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "User");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                if (!user.IsActive)
                {
                    TempData[MessageConstant.ErrorMessage] = "You have been banned!";

                    return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
                }

                var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (HttpContext.Session.GetString("UserId") == null)
                    {
                        HttpContext.Session.SetString("UserId", user.Id);
                    }
                    
                    if (await userManager.IsInRoleAsync(user, "Administrator"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login!");

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateAdmin()
        {
            if (await roleManager.Roles.AnyAsync(r => r.Name == "Administrator") == false)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            var admin = await userManager.FindByEmailAsync("admin@medshop.com");

            if (await userManager.IsInRoleAsync(admin, "Administrator"))
            {
                TempData[MessageConstant.WarningMessage] = "Admin already exists!";
            }

            await userManager.AddToRoleAsync(admin, "Administrator");
            TempData[MessageConstant.SuccessMessage] = "Admin created!";

            return RedirectToAction("Index", "Home");
        }
    }
}
