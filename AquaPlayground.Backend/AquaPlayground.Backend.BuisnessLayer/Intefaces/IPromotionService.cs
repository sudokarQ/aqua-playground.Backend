namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.Promotion;

    public interface IPromotionService
    {
        Task CreateAsync(PromotionPostDto service);
        
        Task<PromotionGetDto> FindByIdAsync(Guid id);
        
        Task<List<PromotionGetDto>> GetAllAsync();
        
        Task<List<PromotionGetDto>> GetListByNameAsync(string name);
        
        Task UpdateAsync(PromotionPutDto dto);
        
        Task RemoveAsync(Guid id);
    }
}
