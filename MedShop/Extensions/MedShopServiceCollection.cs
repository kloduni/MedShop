using MedShop.Core.Contracts;
using MedShop.Core.Services;
using MedShop.Infrastructure.Data.Common;

namespace MedShop.Extensions.DependencyInjection
{
    public static class MedShopServiceCollection
    {
        public static IServiceCollection AddMedShopServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITraderService, TraderService>();

            return services;
        }
    }
}
