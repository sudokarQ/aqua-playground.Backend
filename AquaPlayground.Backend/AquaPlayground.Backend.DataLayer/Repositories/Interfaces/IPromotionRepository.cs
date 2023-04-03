using AquaPlayground.Backend.Common.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IPromotionRepository : IGenericRepository<Promotion>
    {
        Task<List<Promotion>> GetListByNameAsync(string name);
        Task<Promotion> FindByIdAsync(Guid id);
    }
}
