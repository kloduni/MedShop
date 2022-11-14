﻿using MedShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MedShop.Core.Contracts;
using Microsoft.AspNetCore.Authorization;

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
            var model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}