﻿using AquaPlayground.Backend.Common.Entity;

namespace AquaPlayground.Backend.DataLayer.Repositories.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<List<Service>> GetListByNameAsync(string name);
        Task<Service> FindByIdAsync(Guid id);
    }
}
