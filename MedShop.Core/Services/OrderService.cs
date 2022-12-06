using MedShop.Core.Contracts;
using MedShop.Core.Models.Order;
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

        public async Task<ICollection<AllOrdersServiceModel>> GetOrdersModelByUserIdAsync(string userId)
        {
            var orderItems = await repo.AllReadonly<OrderItem>()
                .Where(oi => oi.Order.UserId == userId)
                .Select(oi => new OrderItemServiceModel()
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    Amount = oi.Amount,
                    ProductName = oi.Product.ProductName
                })
                .ToListAsync();

            return await repo.AllReadonly<Order>()
                .Where(o => o.UserId == userId)
                .Select(o => new AllOrdersServiceModel()
                {
                    Id = o.Id,
                    UserName = o.User.UserName,
                    OrderItems = orderItems,
                    TotalPrice = $"${orderItems.Select(i => i.Price * i.Amount).Sum().ToString()}"
                })
                .ToListAsync();
        }

        public async Task<ShoppingCartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await repo.AllReadonly<ShoppingCartItem>()
                .Where(i => i.Id == cartItemId)
                .FirstOrDefaultAsync();
        }
    }
}
