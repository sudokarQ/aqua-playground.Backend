using AquaPlayground.Backend.Common.Models.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetByUserIdAsync(Guid id);
        Task<Order> FindByIdAsync(Guid id);
    }
}
