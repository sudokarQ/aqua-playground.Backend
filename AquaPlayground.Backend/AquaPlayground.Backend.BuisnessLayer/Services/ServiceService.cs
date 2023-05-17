using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Service;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;

namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;


        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task CreateAsync(ServicePostDto service)
        {
            await _serviceRepository.CreateAsync(new()
                {
                    Name = service.Name,
                    TypeService = service.TypeService,
                    Price = service.Price,
                    Description = service.Description,
                });
        }

        public async Task<List<ServiceGetDto>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            var result = new List<ServiceGetDto>();

            foreach (var service in services)
            {
                var serviceDto = new ServiceGetDto
                {
                    Id = service.Id,
                    Name = service.Name,
                    TypeService = service.TypeService,
                    Price = service.Price,
                    Description = service.Description,
                };

                result.Add(serviceDto);
            }

            return result;
        }

        public async Task<List<ServiceGetDto>?> FindByIdAsync(Guid id)
        {
            var service = await _serviceRepository.FindByIdAsync(id);


            return service is null ? null :
                new List<ServiceGetDto>
                {
                    new ServiceGetDto
                    {
                        Id = service.Id,
                        Name = service.Name,
                        TypeService = service.TypeService,
                        Price = service.Price,
                        Description = service.Description,
                    }
                };
        }

        public async Task<List<ServiceGetDto>> GetListByNameAsync(string name)
        {
            var services = await _serviceRepository.GetAsync(x => x.Name.ToLower().StartsWith(name.ToLower()));

            return services.Select(x => new ServiceGetDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                TypeService = x.TypeService,
                Description = x.Description,
            }).OrderBy(x => x.Name).ToList();
        }

        public async Task RemoveAsync(Guid id)
        {
            var service = await _serviceRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (service is null)
            {
                throw new ArgumentNullException("Service not found");
            }

            await _serviceRepository.RemoveAsync(service);
        }

        public async Task UpdateAsync(ServicePutDto dto)
        {
            var service = await _serviceRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (service is null || (service.Name == dto.Name && service.Price == dto.Price && service.Description == dto.Description))
            {
                throw new ArgumentException("Сервис не найден или присвоены старые значения всех полей");
            }

            service.Name = dto.Name ?? service.Name;
            service.Description = dto.Description ?? service.Description;
            service.Price = dto.Price ?? service.Price;

            await _serviceRepository.UpdateAsync(service);
        }
    }
}
