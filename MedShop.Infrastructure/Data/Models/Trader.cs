using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MedShop.Infrastructure.Data.Models
{
    public class Trader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)] 
        public string TraderName { get; set; } = null!;

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [Required] 
        [ForeignKey(nameof(User))] 
        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
