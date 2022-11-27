using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedShop.Infrastructure.Data.Models;

namespace MedShop.Core.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string Trader { get; set; } = null!;
    }
}
