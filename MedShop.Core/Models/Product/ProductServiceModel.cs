﻿using System.ComponentModel.DataAnnotations;

namespace MedShop.Core.Models.Product
{
    public class ProductServiceModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public int Quantity { get; set; }

        public string Seller { get; set; } = null!;
    }
}
