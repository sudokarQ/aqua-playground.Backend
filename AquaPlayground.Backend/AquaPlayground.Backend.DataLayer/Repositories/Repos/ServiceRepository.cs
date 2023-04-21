using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<Service> _dbSet;

        public ServiceRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Service>();
        }

        public async Task<Service> FindByIdAsync(Guid id)
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Service>> GetListByNameAsync(string name) 
            => await _dbSet.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}

