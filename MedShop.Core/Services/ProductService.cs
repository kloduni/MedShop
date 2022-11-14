using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedShop.Core.Contracts;
using MedShop.Core.Models;
using MedShop.Infrastructure.Data.Common;
using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repo;

        public ProductService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            return await repo.All<Product>()
                .Select(p => new ProductViewModel()
                {
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Trader = p.User.UserName
                })
                .ToListAsync();

        }
    }
}
