namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Enums;
    using Common.Models.Entity;

    using Microsoft.EntityFrameworkCore;

    using Repositories.Interfaces;

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<Order> dbSet;

        public OrderRepository(SqlContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Order>();
        }

        public async Task CreateOrderForCart(Order order, string userId)
        {
            context.Entry(order.User).State = EntityState.Detached;

            order.UserId = userId;

            await dbSet.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
            => await dbSet.AsNoTracking()
                .Include(x => x.OrderServices)
                .ThenInclude(xs => xs.Service)
                .Include(x => x.User).ToListAsync();

        public async Task<Order> FindByIdAsync(Guid id)
            => await dbSet.AsNoTracking()
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Order>> GetByUserIdAsync(string id)
            => await dbSet
            .Include(x => x.User)
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Where(x => x.User.Id == id)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Order> GetClientCart(string id)
            => await dbSet
            .Include(x => x.User)
            .Include(x => x.OrderServices)
            .ThenInclude(xs => xs.Service)
            .Where(x => x.User.Id == id && x.Status == OrderStatus.InCart)
            .AsNoTracking().FirstOrDefaultAsync();

        public async Task<List<Order>> FindByDateAsync(DateTime? begin, DateTime? end, string id)
        {
            IQueryable<Order> query = dbSet
                .Where(x => x.Status == OrderStatus.Ordered)
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
