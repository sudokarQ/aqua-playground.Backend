using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using DiplomaProject.Backend.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AquaPlayground.Backend.BuisnessLayer.Helpers
{
    public static class DI
    {
        public static void Add(IConfiguration conf, IServiceCollection service)
        {
            AddClass(service);
        }


        private static void AddClass(IServiceCollection service)
        {

            service.AddScoped<IAuthService, AuthService>();
        }
    }
}
