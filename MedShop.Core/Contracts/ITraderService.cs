namespace MedShop.Core.Contracts
{
    public interface ITraderService
    {
        Task<bool> ExistsByIdAsync(string userId);
        Task<bool> UserWithTraderNameExists(string traderName);
        Task<bool> UserWithPhoneNumberExists(string phoneNumber);
        Task Create(string userId, string traderName, string phoneNumber);
        Task<int> GetTraderIdAsync(string userId);
    }
}
