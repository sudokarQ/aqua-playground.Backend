namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.Service;

    public interface IServiceService
    {
        Task CreateAsync(ServicePostDto service);

        Task<List<ServiceGetDto>> FindByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task UpdateAsync(ServicePutDto dto);

        Task<List<ServiceGetDto>> GetAllAsync();

        Task<List<ServiceGetDto>> GetListByNameAsync(string name);
    }
}
