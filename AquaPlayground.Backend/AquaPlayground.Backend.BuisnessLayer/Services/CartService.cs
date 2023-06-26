namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    using AutoMapper;

    using Common.Models.Dto.Order;
    using Common.Models.Dto.Service;
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Intefaces;

    public class CartService : ICartService
    {
        private readonly IOrderRepository orderRepository;

        private readonly IServiceRepository serviceRepository;

        private readonly IOrderServiceRepository orderServiceRepository;

        private readonly IOrderService orderService;

        private readonly IMapper mapper;

        public CartService(IOrderRepository orderRepository, 
            IMapper mapper, 
            IServiceRepository serviceRepository, 
            IOrderServiceRepository orderServiceRepository, 
            IOrderService orderService)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
            this.orderServiceRepository = orderServiceRepository;
            this.orderService = orderService;
        }

        public async Task<OrderGetDto> GetClientCart(string userId)
        {
            var order = await orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await orderRepository.GetClientCart(userId);
            }

            var result = mapper.Map<OrderGetDto>(order);

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
            var order = await orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await orderRepository.GetClientCart(userId);
            }

            var service = await serviceRepository.FindByIdAsync(id);

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
            await orderServiceRepository.CreateAsync(orderService);

            await orderRepository.UpdateAsync(order);
        }

        public async Task RemoveServiceFromCart(Guid id, string userId)
        {
            var order = await orderRepository.GetClientCart(userId);

            if (!await DoesOrderExist(order, userId))
            {
                order = await orderRepository.GetClientCart(userId);
            }

            var service = await serviceRepository.FindByIdAsync(id);

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
            await orderServiceRepository.RemoveAsync(orderService);

            await orderRepository.UpdateAsync(order);
        }

        public async Task<Order> OrderFromCart(string userId, string adress)
        {
            var order = await orderRepository.GetClientCart(userId);


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

            await orderRepository.UpdateAsync(order);

            return order;
        }

        public async Task<bool> DoesOrderExist(Order order, string userId)
        {
            if (order is null)
            {
                await orderService.CreateAsync(new OrderPostDto
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
