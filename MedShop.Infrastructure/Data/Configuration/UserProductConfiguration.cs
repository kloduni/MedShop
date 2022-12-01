using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedShop.Infrastructure.Data.Configuration
{
    internal class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
            builder.HasData(CreateUsersProductsList());
        }

        private List<UserProduct> CreateUsersProductsList()
        {
            var usersProducts = new List<UserProduct>()
            {
                new UserProduct()
                {
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    ProductId = 1
                },

                new UserProduct()
                {
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    ProductId = 2
                },

                new UserProduct()
                {
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    ProductId = 3
                },

                new UserProduct()
                {
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    ProductId = 4
                },
            };

            return usersProducts;
        }
    }
}
