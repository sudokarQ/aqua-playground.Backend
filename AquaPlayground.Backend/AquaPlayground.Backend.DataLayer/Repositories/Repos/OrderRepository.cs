using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.Common.Enums;
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

        public async Task<List<Order>> GetAllAsync()
            => await _dbSet.AsNoTracking()
                .Include(x => x.OrderServices)
                .ThenInclude(xs => xs.Service)
                .Include(x => x.User).ToListAsync();

        public async Task<Order> FindByIdAsync(Guid id)
            => await _dbSet.AsNoTracking()
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Order>> GetByUserIdAsync(string id)
            => await _dbSet
            .Include(x => x.User)
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Where(x => x.User.Id == id)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<Order>> GetClientCart(string id)
            => await _dbSet
            .Include(x => x.User)
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Where(x => x.User.Id == id && x.Status == OrderStatus.Added)
            .AsNoTracking()
            .ToListAsync();

        public async Task<List<Order>> FindByDateAsync(DateTime? begin, DateTime? end, string id)
        {
            IQueryable<Order> query = _dbSet
                .Include(x => x.User)
                .Include(x => x.OrderServices)
                .ThenInclude(xs => xs.Service)
                .AsNoTracking();

            if (begin.HasValue && end.HasValue)
            {
                query = query.Where(x => x.DateTime > begin && x.DateTime < end);
            }
            else if (begin.HasValue)
            {
                query = query.Where(x => x.DateTime > begin);
            }
            else if (end.HasValue)
            {
                query = query.Where(x => x.DateTime < end);
            }

            if (id != null)
            {
                query = query.Where(x => x.User != null && x.User.Id == id);
            }

            List<Order> orders = await query.ToListAsync();
            return orders;
        }
    }
}
