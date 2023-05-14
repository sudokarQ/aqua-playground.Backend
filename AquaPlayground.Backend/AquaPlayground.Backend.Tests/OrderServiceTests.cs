using AquaPlayground.Backend.Common.Models.Dto.Order;

namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService _orderService;

        private Mock<IOrderRepository> _orderRepositoryMock;

        private Mock<IServiceRepository> _serviceRepositoryMock;

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
            Status = Common.Enums.OrderStatus.Accepted,
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
            Status = Common.Enums.OrderStatus.Accepted,
            DeliveryAdress = "adress",
            User = _user,
            // Set properties of the order object
        };

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _mapperMock = new Mock<IMapper>();
            _orderService = new BuisnessLayer.Services.OrderService(_orderRepositoryMock.Object, _mapperMock.Object, _serviceRepositoryMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldCreateOrderWithCorrectValues()
        {
            // Arrange
            var serviceId1 = Guid.NewGuid();
            var serviceId2 = Guid.NewGuid();

            var service1 = new Service { Id = serviceId1, Price = 10 };
            var service2 = new Service { Id = serviceId2, Price = 15 };

            var orderPostDto = new OrderPostDto
            {
                Status = Common.Enums.OrderStatus.Accepted,
                DeliveryAdress = "adress",
                ServicesId = new List<Guid> { serviceId1, serviceId2 }
            };

            _serviceRepositoryMock
                .Setup(r => r.FindByIdAsync(serviceId1))
                .ReturnsAsync(service1);

            _serviceRepositoryMock
                .Setup(r => r.FindByIdAsync(serviceId2))
                .ReturnsAsync(service2);

            _mapperMock
                .Setup(m => m.Map<Order>(orderPostDto))
                .Returns(new Order());

            Order createdOrder = null;
            _orderRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Callback<Order>(order => createdOrder = order)
                .Returns(Task.CompletedTask);

            // Act
            await _orderService.CreateAsync(orderPostDto, _user);

            // Assert
            Assert.NotNull(createdOrder);
            Assert.AreEqual(_user, createdOrder.User);
            Assert.AreEqual(orderPostDto.ServicesId.Count, createdOrder.OrderServices.Count);

            // Verify that the expected services were added to the order
            Assert.IsTrue(createdOrder.OrderServices.Any(os => os.ServiceId == serviceId1));
            Assert.IsTrue(createdOrder.OrderServices.Any(os => os.ServiceId == serviceId2));

            _serviceRepositoryMock.Verify(r => r.FindByIdAsync(serviceId1), Times.Once);
            _serviceRepositoryMock.Verify(r => r.FindByIdAsync(serviceId2), Times.Once);
            _orderRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public async Task FindByIdAsync_ShouldReturnOrderGetDto()
        {
            // Arrange
            var orderId = order.Id;

            _orderRepositoryMock
                .Setup(r => r.FindByIdAsync(orderId))
                .ReturnsAsync(order);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(order))
                .Returns(new OrderGetDto
                {
                    Id = order.Id,
                    // Set other properties of the OrderGetDto
                });

            // Act
            var result = await _orderService.FindByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(order.Id, result.Id);
            // Assert other properties of the OrderGetDto

            _orderRepositoryMock.Verify(r => r.FindByIdAsync(orderId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderGetDto>(order), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var orders = new List<Order>
            {
                order,
                order,
            };

            _orderRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(orders);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                    // Set other properties of the OrderGetDto
                });

            // Act
            var result = await _orderService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);

            // Assert individual OrderGetDto properties and mappings

            _orderRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task GetClientCart_ShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var clientId = "1";
            var orders = new List<Order>
            {
                order,
                order,
            };

            _orderRepositoryMock
                .Setup(r => r.GetClientCart(clientId))
                .ReturnsAsync(orders);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                    // Set other properties of the OrderGetDto
                });

            // Act
            var result = await _orderService.GetClientCart(clientId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);

            // Assert individual OrderGetDto properties and mappings

            _orderRepositoryMock.Verify(r => r.GetClientCart(clientId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task GetListByUserIdAsync_ShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var userId = "1";
            var orders = new List<Order>
            {
                order,
                order,
            };

            _orderRepositoryMock
                .Setup(r => r.GetByUserIdAsync(userId))
                .ReturnsAsync(orders);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                    // Set other properties of the OrderGetDto
                });

            // Act
            var result = await _orderService.GetListByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);

            // Assert individual OrderGetDto properties and mappings

            _orderRepositoryMock.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task GetListByDatesAsync_ShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var beginDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now;
            var userId = "1";
            var orders = new List<Order>
            {
                order,
                order,
            };

            _orderRepositoryMock
                .Setup(r => r.FindByDateAsync(beginDate, endDate, userId))
                .ReturnsAsync(orders);

            _mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                    // Set other properties of the OrderGetDto
                });

            // Act
            var result = await _orderService.GetListByDatesAsync(beginDate, endDate, userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);

            // Assert individual OrderGetDto properties and mappings

            _orderRepositoryMock.Verify(r => r.FindByDateAsync(beginDate, endDate, userId), Times.Once);
            _mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task RemoveAsync_ShouldRemoveOrder()
        {
            var testOrder = order;
            _orderRepositoryMock
                .Setup(r => r.FindByIdAsync(testOrder.Id))
                .ReturnsAsync(order);

            _orderRepositoryMock
                .Setup(r => r.RemoveAsync(testOrder))
                .Returns(Task.CompletedTask);

            // Act
            await _orderService.RemoveAsync(testOrder.Id);

            // Assert
            _orderRepositoryMock.Verify(r => r.FindByIdAsync(testOrder.Id), Times.Once);
            _orderRepositoryMock.Verify(r => r.RemoveAsync(testOrder), Times.Once);
        }
    }
}