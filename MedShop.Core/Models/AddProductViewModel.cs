using MedShop.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedShop.Core.Models
{
    public class AddProductViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string ProductName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(0, 999999)]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}
