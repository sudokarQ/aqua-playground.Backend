namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetByUserIdAsync(string id);

        Task<Order> FindByIdAsync(Guid id);

        Task<List<Order>> FindByDateAsync(DateTime? begin, DateTime? end, string id);

        Task<Order> GetClientCart(string id);

        Task<List<Order>> GetAllAsync();
    }
}
