using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedShop.Core.Models.Product
{
    public class ProductQueryModel
    {
        public int TotalProductsCount { get; set; }

        public IEnumerable<ProductServiceModel> Products { get; set; } = new List<ProductServiceModel>();
    }
}
