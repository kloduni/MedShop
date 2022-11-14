using MedShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedShop.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(CreateUsersList());
        }

        private List<User> CreateUsersList()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin",
                NormalizedUserName = "admin@mail.com",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
            };
            user.PasswordHash = hasher.HashPassword(user, "admin123");

            users.Add(user);

            user = new User()
            {
                Id = "89159c08-2f95-456f-91ea-75136c030b7b",
                UserName = "guest",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };
            user.PasswordHash = hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }
    }
}
