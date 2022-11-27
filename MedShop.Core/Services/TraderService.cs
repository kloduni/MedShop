using MedShop.Core.Contracts;
using MedShop.Infrastructure.Data.Common;
using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedShop.Core.Services
{
    public class TraderService : ITraderService
    {
        private readonly IRepository repo;

        public TraderService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            return await repo.AllReadonly<Trader>().AnyAsync(t => t.UserId == userId);
        }

        public async Task<bool> UserWithTraderNameExists(string traderName)
        {
            return await repo.AllReadonly<Trader>()
                .AnyAsync(t => t.TraderName == traderName);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await repo.AllReadonly<Trader>()
                .AnyAsync(t => t.PhoneNumber == phoneNumber);
        }

        public async Task Create(string userId, string traderName, string phoneNumber)
        {
            var trader = new Trader()
            {
                UserId = userId,
                TraderName = traderName,
                PhoneNumber = phoneNumber
            };

            await repo.AddAsync(trader);
            await repo.SaveChangesAsync();
        }

        public async Task<int> GetTraderIdAsync(string userId)
        {
            return (await repo.All<Trader>()
                .FirstOrDefaultAsync(t => t.UserId == userId))?.Id ?? 0;
        }
    }
}
