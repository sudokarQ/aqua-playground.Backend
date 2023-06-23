namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByIdAsync(string id);

        Task<List<User>> FindByLoginAsync(string login);

        Task<List<User>> FindByNameAsync(string name);
    }
}
