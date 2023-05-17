using AquaPlayground.Backend.Common.Models.Dto.Order;
using AquaPlayground.Backend.Common.Models.Entity;

namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    public interface ICartService
    {
        Task<OrderGetDto> GetClientCart(string userId);

        Task AddServiceToCart(Guid id, string userId);

        Task RemoveServiceFromCart(Guid id, string userId);

        Task<Order> OrderFromCart(string userId, string adress);
    }
}
