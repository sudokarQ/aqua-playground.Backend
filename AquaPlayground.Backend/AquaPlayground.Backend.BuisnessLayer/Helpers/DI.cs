namespace AquaPlayground.Backend.BuisnessLayer.Helpers
{
    using Intefaces;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Services;

    public static class DI
    {
        public static void Add(IConfiguration conf, IServiceCollection service)
        {
            AddClass(service);
        }

        private static void AddClass(IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IServiceService, ServiceService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IPromotionService, PromotionService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<ICartService, CartService>();
        }
    }
}
