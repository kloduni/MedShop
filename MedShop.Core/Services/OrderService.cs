using MedShop.Core.Contracts;
using MedShop.Infrastructure.Data.Common;
using MedShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;

        public OrderService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task StoreOrderAsync(ICollection<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };
                await repo.AddAsync(orderItem);
            }

            await repo.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await repo.AllReadonly<Order>()
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .Include(o => o.User)
                .ToListAsync();

            return orders;
        }

        public async Task<ShoppingCartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await repo.AllReadonly<ShoppingCartItem>()
                .Where(i => i.Id == cartItemId)
                .FirstOrDefaultAsync();
        }
    }
}
