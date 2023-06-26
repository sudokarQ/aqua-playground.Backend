namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.Order;

    public interface IOrderService
    {
        Task CreateAsync(OrderPostDto order, string userId);
        
        Task RemoveAsync(Guid id);
        
        Task UpdateAsync(OrderPutDto dto);
        
        Task<List<OrderGetDto>> GetAllAsync();
        
        Task<OrderGetDto> FindByIdAsync(Guid id);
        
        Task<List<OrderGetDto>> GetListByUserIdAsync(string id);
        
        Task<List<OrderGetDto>> GetListByDatesAsync(DateTime? begin, DateTime? end, string id);
    }
}
