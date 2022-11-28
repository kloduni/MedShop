using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MedShop.Infrastructure.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserProduct> UsersProducts { get; set; } = new List<UserProduct>();
    }
}
