namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IOrderPromotionRepository : IGenericRepository<OrderPromotion>
    {
        Task RemoveRangeAsync(List<OrderPromotion> entities);
    }
}
