using AquaPlayground.Backend.Common.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<Order> _dbSet;

        public OrderRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Order>();
        }

        public async Task<Order> FindByIdAsync(Guid id)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Order>> GetByUserIdAsync(Guid id)
            => await _dbSet
            .Include(x => x.User)
            .Include(x => x.Service)
            .Where(x => x.User.Id == id.ToString())
            .AsNoTracking()
            .ToListAsync();
    }
}
