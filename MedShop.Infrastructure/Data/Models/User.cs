using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MedShop.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(255)]
        public string? ImageUrl { get; set; } 

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
