using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using AquaPlayground.Backend.DataLayer.Repositories.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AquaPlayground.Backend.DataLayer.Helpers
{
    public static class DI
    {
        public static void Add(IConfiguration conf, IServiceCollection service)
        {
            AddContext(conf, service);
            AddClass(service);
        }

        private static void AddContext(IConfiguration conf, IServiceCollection service)
        {
            string connection = conf.GetConnectionString("AquaPlayground");

            service.AddDbContext<SqlContext>(options => options.UseSqlServer(connection).EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
        }

        private static void AddClass(IServiceCollection service)
        {
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IServiceRepository, ServiceRepository>();
            service.AddScoped<IPromotionRepository, PromotionRepository>();
            service.AddScoped<IOrderPromotionRepository, OrderPromotionRepository>();
            service.AddScoped<IOrderServiceRepository, OrderServiceRepository>();
        }
    }
}
