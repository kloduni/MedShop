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
        Task<int> CreateAsync(ProductBaseModel model, string userId);
        Task<bool> ExistsAsync(int productId);
        Task<ProductServiceModel> ProductDetailsByIdAsync(int productId);
        Task<IEnumerable<ProductServiceModel>> AllProductsByUserIdAsync(string userId);
        Task<bool> HasUserWithIdAsync(int productId, string currentUserId);
        Task<int> GetProductCategoryIdAsync(int productId);
        Task EditAsync(int productId, ProductBaseModel model);
        Task DeleteAsync(int productId);
    }
}
