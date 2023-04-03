using AquaPlayground.Backend.Common.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SqlContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(SqlContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<User> FindByIdAsync(Guid id) 
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id.ToString());
        public async Task<List<User>> FindByLoginAsync(string login) 
            => await _dbSet.AsNoTracking().Where(x => x.UserName.ToLower().Contains(login.ToLower())).ToListAsync();
        public async Task<List<User>> FindByNameAsync(string name) 
            => await _dbSet.AsNoTracking().Where(x => x.Name.ToLower().StartsWith(name.ToLower()) || x.Surname.ToLower().StartsWith(name.ToLower())).ToListAsync();
    }
}
