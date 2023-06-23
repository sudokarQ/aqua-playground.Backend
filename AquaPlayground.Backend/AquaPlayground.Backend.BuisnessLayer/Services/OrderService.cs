namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    using AutoMapper;

    using Common.Models.Dto.Order;
    using Common.Models.Dto.Service;
    using Common.Models.Entity;

    using DataLayer.Repositories.Interfaces;

    using Intefaces;

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        private readonly IMapper mapper;

        private readonly IServiceRepository serviceRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
        }

        public async Task CreateAsync(OrderPostDto orderDto, string userId)
        {
            var order = mapper.Map<Order>(orderDto);

            decimal sum = 0;
            foreach (var serviceId in orderDto.ServicesId)
            {
                var service = await serviceRepository.FindByIdAsync(serviceId);
                if (service != null)
                {
                    var orderService = new Common.Models.Entity.OrderService { Order = order, ServiceId = serviceId };
                    order.OrderServices.Add(orderService);

                    sum += service.Price;
                }
            }

            order.UserId = userId;
            order.DateTime = DateTime.Now;
            order.TotalPrice = sum;

            await orderRepository.CreateAsync(order);
        }


        public async Task<OrderGetDto> FindByIdAsync(Guid id)
        {
            var order = await orderRepository.FindByIdAsync(id);

            var result = mapper.Map<OrderGetDto>(order);

            result.PhoneNumber = order.User.PhoneNumber;

            result.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
            {
                Name = os.Service.Name,
                Price = os.Service.Price
            }).ToList();
            return result;
        }

        public async Task<List<OrderGetDto>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllAsync();

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = mapper.Map<OrderGetDto>(order);

                temp.PhoneNumber = order.User.PhoneNumber;

                temp.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
                {
                    Name = os.Service.Name,
                    Price = os.Service.Price
                }).ToList();

                result.Add(temp);
            }

            return result;
        }

        public async Task<List<OrderGetDto>> GetListByUserIdAsync(string id)
        {
            var orders = await orderRepository.GetByUserIdAsync(id);

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = mapper.Map<OrderGetDto>(order);

                temp.PhoneNumber = order.User.PhoneNumber;

                temp.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
                {
                    Name = os.Service.Name,
                    Price = os.Service.Price
                }).ToList();

                result.Add(temp);
            }

            return result;
        }

        public async Task<List<OrderGetDto>> GetListByDatesAsync(DateTime? begin, DateTime? end, string? id)
        {
            if (begin is not null)
                begin = begin.Value.AddDays(1);

            if (end is not null)
                end = end.Value.AddDays(1);

            var orders = await orderRepository.FindByDateAsync(begin, end, id);

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = mapper.Map<OrderGetDto>(order);

                temp.PhoneNumber = order.User.PhoneNumber;

                temp.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
                {
                    Id = os.Service.Id,
                    Name = os.Service.Name,
                    Price = os.Service.Price
                }).ToList();

                result.Add(temp);
            }

            return result;
        }

        public async Task RemoveAsync(Guid id)
        {
            var order = await orderRepository.FindByIdAsync(id);

            if (order is null)
            {
                throw new ArgumentNullException("Order not found");
            }

            foreach (Common.Models.Entity.OrderService orderService in order.OrderServices)
            {
                orderService.Service = null;
            }

            await orderRepository.RemoveAsync(order);
        }

        public async Task<bool> DoesOrderExist(Order order, string userId)
        {
            if (order is null)
            {
                await CreateAsync(new OrderPostDto
                {
                    Status = Common.Enums.OrderStatus.InCart,
                    DeliveryAdress = "Empty",
                }, userId);

                return false;
            }

            return true;
        }
        public Task UpdateAsync(OrderPutDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
