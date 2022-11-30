using System;
using System.Collections.Generic;
using System.Linq;
using MedShop.Core.Contracts;
using MedShop.Core.Models.Product;
using MedShop.Core.Models.Product.ProductSortingEnum;
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

        public async Task<ProductQueryModel> All(string? category = null, string? searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int productsPerPage = 1)
        {
            var result = new ProductQueryModel();

            var products = repo.AllReadonly<Product>()
                .Where(p => p.IsActive);

            if (string.IsNullOrEmpty(category) == false)
            {
                products = products.Where(p => p.Category.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";
                products = products
                    .Where(p => EF.Functions.Like(p.ProductName.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Description.ToLower(), searchTerm) ||
                                EF.Functions.Like(p.Category.Name.ToLower(), searchTerm));
            }

            products = sorting switch
            {
                ProductSorting.Price => products.OrderBy(p => p.Price),
                _ => products.OrderByDescending(p => p.Id)
            };

            result.Products = await products
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                    Seller = p.UsersProducts.Select(up => up.User.UserName).First()
                })
                .ToListAsync();

            result.TotalProductsCount = await products.CountAsync();

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repo.AllReadonly<Category>()
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategoryModel>> AllCategoriesAsync()
        {
            return await repo.AllReadonly<Category>()
                .OrderBy(c => c.Name)
                .Select(c => new ProductCategoryModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repo.AllReadonly<Category>()
                .AnyAsync(c => c.Id == categoryId);

        }

        public async Task<int> CreateAsync(ProductBaseModel model, string userId)
        {
            var product = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            var userProduct = new UserProduct()
            {
                UserId = userId,
                Product = product
            };

            await repo.AddAsync(product);
            await repo.AddAsync(userProduct);
            await repo.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> ExistsAsync(int productId)
        {
            return await repo.AllReadonly<Product>()
                .AnyAsync(p => p.Id == productId);
        }

        public async Task<ProductServiceModel> ProductDetailsByIdAsync(int productId)
        {
            return await repo.AllReadonly<Product>()
                .Where(p => p.IsActive && p.Id == productId)
                .Select(p => new ProductServiceModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                    Seller = p.UsersProducts.Select(up => up.User.UserName).First()
                })
                .FirstAsync();
        }

        public async Task<IEnumerable<ProductServiceModel>> AllProductsByUserIdAsync(string userId)
        {
            return await repo.AllReadonly<Product>()
                .Where(p => p.IsActive && p.UsersProducts.Select(up => up.UserId).First() == userId)
                .Select(p => new ProductServiceModel()
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                    Seller = p.UsersProducts.Select(up => up.User.UserName).First()
                })
                .ToListAsync();
        }

        public async Task<bool> HasUserWithIdAsync(int productId, string currentUserId)
        {
            bool result = false;

            var product = await repo.AllReadonly<Product>()
                .Where(p => p.IsActive && p.Id == productId)
                .Include(p => p.UsersProducts)
                .FirstOrDefaultAsync();

            if (product != null && product.UsersProducts.Select(up => up.UserId).First() == currentUserId)
            {
                result = true;
            }

            return result;
        }

        public async Task<int> GetProductCategoryIdAsync(int productId)
        {
            return (await repo.GetByIdAsync<Product>(productId)).CategoryId;
        }

        public async Task EditAsync(int productId, ProductBaseModel model)
        {
            var product = await repo.GetByIdAsync<Product>(productId);

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await repo.GetByIdAsync<Product>(productId);
            product.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await repo.All<Product>()
                .Include(p => p.UsersProducts)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}
