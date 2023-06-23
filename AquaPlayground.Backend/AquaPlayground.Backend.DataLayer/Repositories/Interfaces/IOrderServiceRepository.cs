namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IOrderServiceRepository : IGenericRepository<OrderService>
    {
        Task RemoveRangeAsync(List<OrderService> entities);
    }
}
