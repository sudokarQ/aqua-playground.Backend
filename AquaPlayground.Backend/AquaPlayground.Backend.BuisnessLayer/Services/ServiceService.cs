﻿namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    using Common.Models.Dto.Service;

    using DataLayer.Repositories.Interfaces;

    using Intefaces;


    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;


        public ServiceService(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public async Task CreateAsync(ServicePostDto service)
        {
            await serviceRepository.CreateAsync(new()
            {
                Name = service.Name,
                TypeService = service.TypeService,
                Price = service.Price,
                Description = service.Description,
            });
        }

        public async Task<List<ServiceGetDto>> GetAllAsync()
        {
            var services = await serviceRepository.GetAllAsync();
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
            var service = await serviceRepository.FindByIdAsync(id);


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
            var services = await serviceRepository.GetAsync(x => x.Name.ToLower().StartsWith(name.ToLower()));

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
            var service = await serviceRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (service is null)
            {
                throw new ArgumentNullException("Service not found");
            }

            await serviceRepository.RemoveAsync(service);
        }

        public async Task UpdateAsync(ServicePutDto dto)
        {
            var service = await serviceRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (service is null)
            {
                throw new ArgumentException("Serivce not found");
            }

            service.Name = dto.Name ?? service.Name;
            service.Description = dto.Description ?? service.Description;
            service.Price = dto.Price ?? service.Price;

            await serviceRepository.UpdateAsync(service);
        }
    }
}
