using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<Promotion> _dbSet;

        public PromotionRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Promotion>();
        }

        public async Task<Promotion> FindByIdAsync(Guid id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Promotion>> GetListByNameAsync(string name)
            => await _dbSet.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}
