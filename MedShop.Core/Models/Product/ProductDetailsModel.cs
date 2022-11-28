using MedShop.Core.Models.Trader;

namespace MedShop.Core.Models.Product
{
    public class ProductDetailsModel : ProductServiceModel
    {
        public TraderServiceModel Trader { get; set; } = null!;
    }
}
