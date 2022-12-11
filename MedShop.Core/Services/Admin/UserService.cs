using MedShop.Core.Contracts.Admin;
using MedShop.Core.Models.Admin;
using MedShop.Infrastructure.Data.Common;
using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedShop.Core.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;


        public UserService(IRepository _repo)
        {
            repo = _repo;
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

        public async Task BanUserAsync(User user)
        {
            user.IsActive = false;

            await repo.SaveChangesAsync();
        }

        public async Task UnbanUserAsync(User user)
        {
            user.IsActive = true;

            await repo.SaveChangesAsync();
        }
    }
}
