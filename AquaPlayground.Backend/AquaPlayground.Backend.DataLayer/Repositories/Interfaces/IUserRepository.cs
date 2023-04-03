using AquaPlayground.Backend.Common.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByIdAsync(Guid id);
        Task<List<User>> FindByLoginAsync(string login);
        Task<List<User>> FindByNameAsync(string name);
    }
}
