using MedShop.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedShop.Core.Contracts
{
    public interface IOrderService
    {
        Task StoreOrderAsync(ICollection<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<ICollection<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
