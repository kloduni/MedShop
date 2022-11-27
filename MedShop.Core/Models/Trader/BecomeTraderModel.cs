using System.ComponentModel.DataAnnotations;

namespace MedShop.Core.Models.Trader
{
    public class BecomeTraderModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [Display(Name = "Trade name")]
        public string TraderName { get; set; } = null!;

        [Required]
        [StringLength(15, MinimumLength = 7)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
