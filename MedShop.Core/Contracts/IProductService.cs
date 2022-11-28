using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedShop.Core.Models.Product;
using MedShop.Core.Models.Product.ProductSortingEnum;

namespace MedShop.Core.Contracts
{
    public interface IProductService
    {
        Task<ProductQueryModel> All(
            string? category = null,
            string? searchTerm = null,
            ProductSorting sorting = ProductSorting.Newest,
            int currentPage = 1,
            int productsPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();
        Task<IEnumerable<ProductCategoryModel>> AllCategoriesAsync();
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<int> CreateAsync(ProductBaseModel model, int traderId);
        Task<bool> ExistsAsync(int productId);
        Task<ProductDetailsModel> ProductDetailsByIdAsync(int productId);
    }
}
