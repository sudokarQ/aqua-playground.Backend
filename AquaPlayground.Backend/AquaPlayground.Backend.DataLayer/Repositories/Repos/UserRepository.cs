namespace AquaPlayground.Backend.DataLayer.Repositories.Repos
{
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SqlContext context;

        private readonly DbSet<User> dbSet;

        private readonly UserManager<User> userManager;


        public UserRepository(SqlContext context, UserManager<User> userManager) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<User>();
            this.userManager = userManager;
        }

        public async Task<User> FindByIdAsync(string id)
            => await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<User>> FindByLoginAsync(string login)
            => await dbSet.AsNoTracking().Where(x => x.UserName.ToLower().Contains(login.ToLower())).ToListAsync();

        public async Task<List<User>> FindByNameAsync(string name)
            => await dbSet.AsNoTracking().Where(x => x.Name.ToLower().StartsWith(name.ToLower()) || x.Surname.ToLower().StartsWith(name.ToLower())).ToListAsync();
    }
}
