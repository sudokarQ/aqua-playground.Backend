using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class OrderPromotionRepository : GenericRepository<OrderPromotion>, IOrderPromotionRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<OrderPromotion> _dbSet;

        public OrderPromotionRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<OrderPromotion>();
        }

        public async Task RemoveRangeAsync(List<OrderPromotion> entities)
        {
            _context.OrderPromotions.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

    }
}
