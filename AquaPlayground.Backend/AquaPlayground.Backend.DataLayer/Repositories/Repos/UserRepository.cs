using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SqlContext _context;
        
        private readonly DbSet<User> _dbSet;
        
        private readonly UserManager<User> _userManager;


        public UserRepository(SqlContext context, UserManager<User> userManager) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
            _userManager = userManager;
        }

        public async Task<User> FindByIdAsync(string id) 
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<User>> FindByLoginAsync(string login) 
            => await _dbSet.AsNoTracking().Where(x => x.UserName.ToLower().Contains(login.ToLower())).ToListAsync();
        
        public async Task<List<User>> FindByNameAsync(string name) 
            => await _dbSet.AsNoTracking().Where(x => x.Name.ToLower().StartsWith(name.ToLower()) || x.Surname.ToLower().StartsWith(name.ToLower())).ToListAsync();
    }
}
