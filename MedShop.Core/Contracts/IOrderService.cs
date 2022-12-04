using MedShop.Core.Models.Order;
using MedShop.Infrastructure.Data.Models;

namespace MedShop.Core.Contracts
{
    public interface IOrderService
    {
        Task StoreOrderAsync(ICollection<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<ICollection<Order>> GetOrdersByUserIdAsync(string userId);
        Task<ShoppingCartItem> GetCartItemByIdAsync(int cartItemId);
        Task<ICollection<AllOrdersServiceModel>> GetOrdersModelByUserIdAsync(string userId);
    }
}
