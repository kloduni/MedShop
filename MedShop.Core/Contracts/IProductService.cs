using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedShop.Core.Models;

namespace MedShop.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    }
}
