using AquaPlayground.Backend.Common.Models.Dto.Order;

namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private IOrderService _orderService;

        private ICartService _cartService;

        private Mock<IOrderRepository> _orderRepositoryMock;

        private Mock<IServiceRepository> _serviceRepositoryMock;

        private Mock<IOrderServiceRepository> _orderServiceRepositoryMock;

        private Mock<IMapper> _mapperMock;


        private static User _user = new User
        {
            Id = "1",
            Name = "John",
            Surname = "Smith",
        };

        private static List<Service> services = new List<Service>
        {
            new Service { Id = Guid.NewGuid(), Price = 10 },
            new Service { Id = Guid.NewGuid(), Price = 15 }
        };

        private static OrderPostDto _orderPostDto = new OrderPostDto
        {
            Status = Common.Enums.OrderStatus.Ordered,
            DeliveryAdress = "adress",
            ServicesId = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),

            }
        };

        private static Order order = new Order
        {
            Id = Guid.NewGuid(),
            Status = Common.Enums.OrderStatus.Ordered,
            DeliveryAdress = "adress",
            User = _user,
        };

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();

            _serviceRepositoryMock = new Mock<IServiceRepository>();
            
            _orderServiceRepositoryMock = new Mock<IOrderServiceRepository>();
            
            _mapperMock = new Mock<IMapper>();
            
            _orderService = new BuisnessLayer.Services.OrderService(_orderRepositoryMock.Object, _mapperMock.Object, _serviceRepositoryMock.Object);
            
            _cartService = new CartService(_orderRepositoryMock.Object, _mapperMock.Object, _serviceRepositoryMock.Object, _orderServiceRepositoryMock.Object, _orderService);
        }
        
        [Test]
        public async Task GetClientCart_ShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var clientId = "1";
            var testOrder = order;

            _orderRepositoryMock
                .Setup(r => r.GetClientCart(clientId))
                .ReturnsAsync(testOrder);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await _cartService.GetClientCart(clientId);

            // Assert
            Assert.NotNull(result);

            _orderRepositoryMock.Verify(r => r.GetClientCart(clientId), Times.Once);
        }

        [Test]
        public async Task AddServiceToCart_ExistingService_ShouldAddServiceToOrder()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var userId = "1";
            var order = new Order { Id = Guid.NewGuid() };
            var service = new Service { Id = serviceId, Price = 20 };

            _orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            _serviceRepositoryMock
                .Setup(s => s.FindByIdAsync(serviceId))
                .ReturnsAsync(service);

            // Act
            await _cartService.AddServiceToCart(serviceId, userId);

            // Assert
            Assert.AreEqual(20, order.TotalPrice);

            _orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
            _serviceRepositoryMock.Verify(s => s.FindByIdAsync(serviceId), Times.Once);
            _orderServiceRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Common.Models.Entity.OrderService>()), Times.Once);
            _orderRepositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
        }

        [Test]
        public async Task OrderFromCart_ValidOrder_ShouldReturnOrderedOrder()
        {
            // Arrange
            var userId = "1";
            var address = "Some address";
            var id = Guid.NewGuid();
            var order = new Order { Id = id, OrderServices = new List<Common.Models.Entity.OrderService>
            {
                new Common.Models.Entity.OrderService
                {
                    ServiceId = services.FirstOrDefault().Id,
                    Service = services.FirstOrDefault(),
                    OrderId = id,
                }
            }
            };

            _orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            // Act
            var result = await _cartService.OrderFromCart(userId, address);

            // Assert
            Assert.AreEqual(Common.Enums.OrderStatus.Ordered, result.Status);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(address, result.DeliveryAdress);
            Assert.AreNotEqual(DateTime.MinValue, result.DateTime);

            _orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
            _orderRepositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
        }

        [Test]
        public async Task OrderFromCart_ShouldReturnOrderNoServices()
        {
            // Arrange
            var userId = "1";
            var address = "Some address";
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
            };

            _orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _cartService.OrderFromCart(userId, address));

            _orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
        }
    }
}