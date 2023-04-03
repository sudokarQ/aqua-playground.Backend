using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SqlContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SqlContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync() 
            => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).AsNoTracking().ToListAsync();

        public async Task UpdateAsync(TEntity item)
        {
            _dbSet.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) 
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) 
            => await _dbSet.AnyAsync(predicate);
    }
}
