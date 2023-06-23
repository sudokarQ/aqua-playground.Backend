namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using System.Linq.Expressions;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SqlContext context;

        private readonly DbSet<TEntity> dbSet;

        public GenericRepository(SqlContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity item)
        {
            await dbSet.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
            => await dbSet.AsNoTracking().ToListAsync();

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await dbSet.Where(predicate).AsNoTracking().ToListAsync();

        public async Task UpdateAsync(TEntity item)
        {
            dbSet.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await dbSet.AnyAsync(predicate);
    }
}
