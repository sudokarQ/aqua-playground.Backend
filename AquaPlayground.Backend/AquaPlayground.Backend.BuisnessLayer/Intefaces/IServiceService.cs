using AquaPlayground.Backend.Common.Models.Dto.Service;

namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    public interface IServiceService
    {
        Task CreateAsync(ServicePostDto service);
        Task<List<ServiceGetDto>> FindByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(ServicePutDto dto);
        Task<List<ServiceGetDto>> GetAllAsync();
        Task<List<ServiceSearchGetDto>> GetListByNameAsync(string name);
    }
}
