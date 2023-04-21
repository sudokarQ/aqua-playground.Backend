using AquaPlayground.Backend.Common.Models.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IOrderPromotionRepository : IGenericRepository<OrderPromotion>
    {
        Task RemoveRangeAsync(List<OrderPromotion> entities);
    }
}
