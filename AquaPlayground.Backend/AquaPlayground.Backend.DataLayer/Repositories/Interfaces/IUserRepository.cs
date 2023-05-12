using AquaPlayground.Backend.Common.Models.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByIdAsync(string id);

        Task<List<User>> FindByLoginAsync(string login);
        
        Task<List<User>> FindByNameAsync(string name);
    }
}
