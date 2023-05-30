using AquaPlayground.Backend.Common.Models.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IOrderServiceRepository : IGenericRepository<OrderService>
    {
        Task RemoveRangeAsync(List<OrderService> entities);
    }
}
