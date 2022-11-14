using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedShop.Infrastructure.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required] 
        [ForeignKey(nameof(User))] 
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
