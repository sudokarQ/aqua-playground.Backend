namespace AquaPlayground.Backend.Tests
{
    using Common.Models.Dto.Order;
    
    [TestFixture]
    public class CartServiceTests
    {
        private IOrderService orderService;

        private ICartService cartService;

        private Mock<IOrderRepository> orderRepositoryMock;

        private Mock<IServiceRepository> serviceRepositoryMock;

        private Mock<IOrderServiceRepository> orderServiceRepositoryMock;

        private Mock<IMapper> mapperMock;


        private static User user = new User
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

        private static Order order = new Order
        {
            Id = Guid.NewGuid(),
            Status = Common.Enums.OrderStatus.Ordered,
            DeliveryAdress = "adress",
            User = user,
        };

        [SetUp]
        public void Setup()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();

            serviceRepositoryMock = new Mock<IServiceRepository>();
            
            orderServiceRepositoryMock = new Mock<IOrderServiceRepository>();
            
            mapperMock = new Mock<IMapper>();
            
            orderService = new BuisnessLayer.Services.OrderService(orderRepositoryMock.Object, mapperMock.Object, serviceRepositoryMock.Object);
            
            cartService = new CartService(orderRepositoryMock.Object, mapperMock.Object, serviceRepositoryMock.Object, orderServiceRepositoryMock.Object, orderService);
        }
        
        [Test]
        public async Task GetClientCartShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var clientId = "1";
            var testOrder = order;

            orderRepositoryMock
                .Setup(r => r.GetClientCart(clientId))
                .ReturnsAsync(testOrder);

            mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await cartService.GetClientCart(clientId);

            // Assert
            Assert.NotNull(result);

            orderRepositoryMock.Verify(r => r.GetClientCart(clientId), Times.Once);
        }

        [Test]
        public async Task AddServiceToCartExistingServiceShouldAddServiceToOrder()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var userId = "1";
            var order = new Order { Id = Guid.NewGuid() };
            var service = new Service { Id = serviceId, Price = 20 };

            orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            serviceRepositoryMock
                .Setup(s => s.FindByIdAsync(serviceId))
                .ReturnsAsync(service);

            // Act
            await cartService.AddServiceToCart(serviceId, userId);

            // Assert
            Assert.AreEqual(20, order.TotalPrice);

            orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
            serviceRepositoryMock.Verify(s => s.FindByIdAsync(serviceId), Times.Once);
            orderServiceRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Common.Models.Entity.OrderService>()), Times.Once);
            orderRepositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
        }

        [Test]
        public async Task OrderFromCartValidOrderShouldReturnOrderedOrder()
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

            orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            // Act
            var result = await cartService.OrderFromCart(userId, address);

            // Assert
            Assert.AreEqual(Common.Enums.OrderStatus.Ordered, result.Status);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(address, result.DeliveryAdress);
            Assert.AreNotEqual(DateTime.MinValue, result.DateTime);

            orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
            orderRepositoryMock.Verify(r => r.UpdateAsync(order), Times.Once);
        }

        [Test]
        public async Task OrderFromCartShouldReturnOrderNoServices()
        {
            // Arrange
            var userId = "1";
            var address = "Some address";
            var id = Guid.NewGuid();
            var order = new Order
            {
                Id = id,
            };

            orderRepositoryMock
                .Setup(r => r.GetClientCart(userId))
                .ReturnsAsync(order);

            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await cartService.OrderFromCart(userId, address));

            orderRepositoryMock.Verify(r => r.GetClientCart(userId), Times.Once);
        }
    }
}