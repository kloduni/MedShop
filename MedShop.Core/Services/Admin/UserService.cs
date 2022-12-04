using System.Diagnostics.CodeAnalysis;
using MedShop.Core.Contracts.Admin;
using MedShop.Core.Models.Admin;
using MedShop.Infrastructure.Data.Common;
using MedShop.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedShop.Core.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;
        private readonly UserManager<User> userManager;

        public UserService(IRepository _repo, UserManager<User> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task<IEnumerable<UserServiceModel>> All()
        {
            return await repo.AllReadonly<User>()
                .Select(u => new UserServiceModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task BanUserByIdAsync(User user)
        {
            user.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task UnbanUserByIdAsync(User user)
        {
            user.IsActive = true;

            await repo.SaveChangesAsync();
        }
    }
}
