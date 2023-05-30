using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class OrderServiceRepository : GenericRepository<OrderService>, IOrderServiceRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<OrderService> _dbSet;

        public OrderServiceRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<OrderService>();
        }

        public async Task RemoveRangeAsync(List<OrderService> entities)
        {
            _context.OrderServices.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

    }
}
