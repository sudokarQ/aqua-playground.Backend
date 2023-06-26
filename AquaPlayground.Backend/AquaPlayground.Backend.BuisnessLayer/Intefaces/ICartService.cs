namespace AquaPlayground.Backend.BuisnessLayer.Intefaces
{
    using Common.Models.Dto.Order;
    using Common.Models.Entity;

    public interface ICartService
    {
        Task<OrderGetDto> GetClientCart(string userId);

        Task AddServiceToCart(Guid id, string userId);

        Task RemoveServiceFromCart(Guid id, string userId);

        Task<Order> OrderFromCart(string userId, string adress);
    }
}
