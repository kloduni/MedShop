using Microsoft.AspNetCore.Identity;

namespace MedShop.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserProduct> UsersProducts { get; set; } = new List<UserProduct>();
    }
}
