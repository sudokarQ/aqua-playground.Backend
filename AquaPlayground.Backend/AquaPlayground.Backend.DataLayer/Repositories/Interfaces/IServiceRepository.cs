namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<List<Service>> GetListByNameAsync(string name);
        Task<Service> FindByIdAsync(Guid id);
    }
}
