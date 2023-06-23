namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Models.Entity;

    using Microsoft.EntityFrameworkCore;

    using Repositories.Interfaces;

    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<Promotion> dbSet;

        public PromotionRepository(SqlContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Promotion>();
        }

        public async Task<Promotion> FindByIdAsync(Guid id) => await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Promotion>> GetListByNameAsync(string name)
            => await dbSet.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}
