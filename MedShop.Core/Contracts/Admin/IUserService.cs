using System.Diagnostics.CodeAnalysis;
using MedShop.Core.Models.Admin;
using MedShop.Infrastructure.Data.Models;

namespace MedShop.Core.Contracts.Admin
{
    public interface IUserService
    {
        Task<IEnumerable<UserServiceModel>> All();
        Task BanUserByIdAsync(User user);
        Task UnbanUserByIdAsync(User user);
    }
}
