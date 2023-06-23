namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<Service> dbSet;

        public ServiceRepository(SqlContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Service>();
        }

        public async Task<Service> FindByIdAsync(Guid id)
            => await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Service>> GetListByNameAsync(string name)
            => await dbSet.AsNoTracking().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}

