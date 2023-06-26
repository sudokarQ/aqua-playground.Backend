namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Models.Entity;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    public class OrderPromotionRepository : GenericRepository<OrderPromotion>, IOrderPromotionRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<OrderPromotion> dbSet;

        public OrderPromotionRepository(SqlContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<OrderPromotion>();
        }

        public async Task RemoveRangeAsync(List<OrderPromotion> entities)
        {
            context.OrderPromotions.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

    }
}
