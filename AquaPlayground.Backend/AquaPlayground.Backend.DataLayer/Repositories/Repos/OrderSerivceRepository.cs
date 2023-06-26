namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Models.Entity;

    using Microsoft.EntityFrameworkCore;

    using Repositories.Interfaces;

    public class OrderServiceRepository : GenericRepository<OrderService>, IOrderServiceRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<OrderService> dbSet;

        public OrderServiceRepository(SqlContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<OrderService>();
        }

        public async Task RemoveRangeAsync(List<OrderService> entities)
        {
            context.OrderServices.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

    }
}
