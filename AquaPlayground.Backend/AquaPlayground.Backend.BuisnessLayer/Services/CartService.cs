using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Order;
using AquaPlayground.Backend.Common.Models.Dto.Service;
using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using AutoMapper;

namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    public class CartService : ICartService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IServiceRepository _serviceRepository;

        private readonly IOrderServiceRepository _orderServiceRepository;

        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public CartService(IOrderRepository orderRepository, IMapper mapper, IServiceRepository serviceRepository, IOrderServiceRepository orderServiceRepository, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
            _orderServiceRepository = orderServiceRepository;
            _orderService = orderService;
        }

        public async Task<OrderGetDto> GetClientCart(string userId)
        {
            var order = await _orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await _orderRepository.GetClientCart(userId);
            }

            var result = _mapper.Map<OrderGetDto>(order);

            result.PhoneNumber = order.User.PhoneNumber;
            result.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
            {
                Id = os.Service.Id,
                Name = os.Service.Name,
                Price = os.Service.Price
            }).ToList();

            return result;
        }

        public async Task AddServiceToCart(Guid id, string userId)
        {
            var order = await _orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await _orderRepository.GetClientCart(userId);
            }

            var service = await _serviceRepository.FindByIdAsync(id);

            if (service is null)
            {
                throw new ArgumentException("Service not found");
            }

            var orderService = new Common.Models.Entity.OrderService { OrderId = order.Id, ServiceId = service.Id, Service = service };

            order.OrderServices.Add(orderService);

            decimal sum = 0;

            foreach (var item in order.OrderServices)
            {
                sum += item.Service.Price;
            }

            order.TotalPrice = sum;
            order.User = null;
            order.OrderServices = null;

            orderService.Order = null;
            orderService.Service = null;
            await _orderServiceRepository.CreateAsync(orderService);

            await _orderRepository.UpdateAsync(order);
        }

        public async Task RemoveServiceFromCart(Guid id, string userId)
        {
            var order = await _orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await _orderRepository.GetClientCart(userId);
            }

            var service = await _serviceRepository.FindByIdAsync(id);

            if (service is null)
            {
                throw new ArgumentException("Service not found");
            }

            var orderService = order.OrderServices.Find(x => x.OrderId == order.Id && x.ServiceId == service.Id);

            if (orderService is null)
            {
                throw new ArgumentException("Service is not in cart");
            }

            orderService.Order.User = null;
            order.OrderServices.Remove(orderService);

            decimal sum = 0;

            foreach (var item in order.OrderServices)
            {
                sum += item.Service.Price;
            }

            order.TotalPrice = sum;
            order.User = null;

            order.OrderServices = null;

            orderService.Order = null;
            orderService.Service = null;
            await _orderServiceRepository.RemoveAsync(orderService);

            await _orderRepository.UpdateAsync(order);
        }

        public async Task<Order> OrderFromCart(string userId, string adress)
        {
            var order = await _orderRepository.GetClientCart(userId);


            if (order is null || order.OrderServices.Count == 0)
            {
                throw new ArgumentException("No services to order");
            }

            foreach (Common.Models.Entity.OrderService orderService in order.OrderServices)
            {
                orderService.Service = null;
            }

            order.User = null;

            order.UserId = userId;
            order.DateTime = DateTime.Now;
            order.DeliveryAdress = adress;
            order.Status = Common.Enums.OrderStatus.Ordered;

            await _orderRepository.UpdateAsync(order);

            return order;
        }

        public async Task<bool> DoesOrderExist(Order order, string userId)
        {
            if (order is null)
            {
                await _orderService.CreateAsync(new OrderPostDto
                {
                    Status = Common.Enums.OrderStatus.InCart,
                    DeliveryAdress = "Empty",
                }, userId);

                return false;
            }

            return true;
        }
    }
}
