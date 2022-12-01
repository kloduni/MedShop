using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedShop.Infrastructure.Data.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateProducts());
        }

        private List<Product> CreateProducts()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Catheter",
                    Description = "Used for urination complications.",
                    ImageUrl = "https://www.bbraun.com/content/dam/catalog/bbraun/bbraunProductCatalog/S/AEM2015/en-01/b8/vasofix-braunuele.jpeg.transform/75/image.jpg",
                    Price = 13.76m,
                    CategoryId = 1,
                    Quantity = 10
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Spatula",
                    Description = "General instrument.",
                    ImageUrl = "https://www.bbraun-vetcare.com/content/dam/b-braun/global/website/veterinary/products-and-therapies/wound-therapy-and-wound-closure/text_image_nadeln_DLM.jpg.transform/600/image.jpg",
                    Price = 1.50m,
                    CategoryId = 7,
                    Quantity = 0
                },

                new Product
                {
                    Id = 3,
                    ProductName = "Scalpel",
                    Description = "General instrument.",
                    ImageUrl = "https://www.carlroth.com/medias/3607-1000Wx1000H?context=bWFzdGVyfGltYWdlc3w1NjMxNnxpbWFnZS9qcGVnfGltYWdlcy9oOTYvaGM5Lzg4MjIxNDM5NzU0NTQuanBnfGMzZDZlODk0YmE0Y2MyZWE2MmU2ZTA2ZjkxNTNjOGI3MWMyMjgyYzZmNmFjOWFjOTAwMzY5ZjJjNDVkOGEyNTE",
                    Price = 2.50m,
                    CategoryId = 7,
                    Quantity = 10
                },

                new Product
                {
                    Id = 4,
                    ProductName = "Forceps",
                    Description = "General instrument.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Forceps_plastic.jpg/1200px-Forceps_plastic.jpg",
                    Price = 1.00m,
                    CategoryId = 7,
                    Quantity = 10
                }
            };

            return products;
        }
    }
}
