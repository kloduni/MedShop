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
                    Description = "Urology instrument.",
                    ImageUrl = "https://www.bbraun.com/content/dam/catalog/bbraun/bbraunProductCatalog/S/AEM2015/en-01/b8/vasofix-braunuele.jpeg.transform/75/image.jpg",
                    Price = 13.76m,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Spatula",
                    Description = "General instrument.",
                    ImageUrl = "https://www.bbraun-vetcare.com/content/dam/b-braun/global/website/veterinary/products-and-therapies/wound-therapy-and-wound-closure/text_image_nadeln_DLM.jpg.transform/600/image.jpg",
                    Price = 3.50m,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082"
                }
            };

            return products;
        }
    }
}
