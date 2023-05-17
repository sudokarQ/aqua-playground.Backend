using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto.Order;
using AquaPlayground.Backend.Common.Models.Dto.Service;
using AquaPlayground.Backend.Common.Models.Entity;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using AutoMapper;

namespace AquaPlayground.Backend.BuisnessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly IServiceRepository _serviceRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IServiceRepository serviceRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _serviceRepository = serviceRepository;
        }

        public async Task CreateAsync(OrderPostDto orderDto, User user)
        {
            var order = _mapper.Map<Order>(orderDto);
                
            decimal sum = 0;
            foreach (var serviceId in orderDto.ServicesId)
            {
                var service = await _serviceRepository.FindByIdAsync(serviceId);
                if (service != null)
                {
                    var orderService = new Common.Models.Entity.OrderService { Order = order, ServiceId = serviceId };
                    order.OrderServices.Add(orderService);

                    sum += service.Price;
                }
            }

            order.User = user;
            order.DateTime = DateTime.Now;
            order.TotalPrice = sum;

            await _orderRepository.CreateAsync(order);
        }


        public async Task<OrderGetDto> FindByIdAsync(Guid id)
        {
            var order = await _orderRepository.FindByIdAsync(id);

            var result = _mapper.Map<OrderGetDto>(order);

            result.UserSurname = order.User.Surname;
            result.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
            {
                Name = os.Service.Name,
                Price = os.Service.Price
            }).ToList();
            return result;
        }

        public async Task<List<OrderGetDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = _mapper.Map<OrderGetDto>(order);
                temp.UserSurname = order.User.Surname;
                temp.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
                {
                    Name = os.Service.Name,
                    Price = os.Service.Price
                }).ToList();

                result.Add(temp);
            }

            return result;
        }

        public async Task<List<OrderGetDto>> GetClientCart(string id)
        {
            var orders = await _orderRepository.GetClientCart(id);

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = _mapper.Map<OrderGetDto>(order);
                temp.UserSurname = order.User.Surname;
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
            var orders = await _orderRepository.GetByUserIdAsync(id);

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = _mapper.Map<OrderGetDto>(order);
                temp.UserSurname = order.User.Surname;
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
            var orders = await _orderRepository.FindByDateAsync(begin, end, id);

            var result = new List<OrderGetDto>();

            foreach (var order in orders)
            {
                var temp = _mapper.Map<OrderGetDto>(order);
                temp.UserSurname = order.User.Surname;
                temp.Services = order.OrderServices.Select(os => new ServiceSearchGetDto
                {
                    Name = os.Service.Name,
                    Price = os.Service.Price
                }).ToList();

                result.Add(temp);
            }

            return result;
        }

        public async Task RemoveAsync(Guid id)
        {
            var order = await _orderRepository.FindByIdAsync(id);

            if (order is null)
            {
                throw new ArgumentNullException("Promotinon not found");
            }

            foreach (Common.Models.Entity.OrderService orderService in order.OrderServices)
            {
                orderService.Service = null;
            }
            await _orderRepository.RemoveAsync(order);
        }

        public Task UpdateAsync(OrderPutDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
