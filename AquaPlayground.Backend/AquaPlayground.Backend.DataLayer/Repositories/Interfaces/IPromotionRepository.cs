namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    using Common.Models.Entity;

    public interface IPromotionRepository : IGenericRepository<Promotion>
    {
        Task<List<Promotion>> GetListByNameAsync(string name);

        Task<Promotion> FindByIdAsync(Guid id);
    }
}
