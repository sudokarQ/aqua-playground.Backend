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
        }

        private static void AddContext(IConfiguration conf, IServiceCollection service)
        {
            string connection = conf.GetConnectionString("Diplom");

            service.AddDbContext<SqlContext>(options => options.UseSqlServer(connection));
        }

    }
}
