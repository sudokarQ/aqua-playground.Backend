namespace AquaPlayground.Backend.Tests
{
    using Common.Models.Dto.Order;
    
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService orderService;

        private Mock<IOrderRepository> orderRepositoryMock;

        private Mock<IServiceRepository> serviceRepositoryMock;

        private Mock<IUserRepository> userRepositoryMock;

        private Mock<IMapper> mapperMock;


        private static User user = new User
        {
            Id = "1",
            Name = "John",
            Surname = "Smith",
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
            userRepositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper>();
            orderService = new BuisnessLayer.Services.OrderService(orderRepositoryMock.Object, mapperMock.Object, serviceRepositoryMock.Object);
        }

        [Test]
        public async Task CreateAsyncShouldCreateOrderWithCorrectValues()
        {
            // Arrange
            var serviceId1 = Guid.NewGuid();
            var serviceId2 = Guid.NewGuid();

            var service1 = new Service { Id = serviceId1, Price = 10 };
            var service2 = new Service { Id = serviceId2, Price = 15 };

            var orderPostDto = new OrderPostDto
            {
                Status = Common.Enums.OrderStatus.Ordered,
                DeliveryAdress = "adress",
                ServicesId = new List<Guid> { serviceId1, serviceId2 }
            };

            serviceRepositoryMock
                .Setup(r => r.FindByIdAsync(serviceId1))
                .ReturnsAsync(service1);

            serviceRepositoryMock
                .Setup(r => r.FindByIdAsync(serviceId2))
                .ReturnsAsync(service2);

            mapperMock
                .Setup(m => m.Map<Order>(orderPostDto))
                .Returns(new Order());

            Order createdOrder = null;

            orderRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Order>()))
                .Callback<Order>(order => createdOrder = order)
                .Returns(Task.CompletedTask);

            // Act
            await orderService.CreateAsync(orderPostDto, user.Id);

            // Assert
            Assert.NotNull(createdOrder);

            Assert.AreEqual(orderPostDto.ServicesId.Count, createdOrder.OrderServices.Count);

            Assert.IsTrue(createdOrder.OrderServices.Any(os => os.ServiceId == serviceId1));
            Assert.IsTrue(createdOrder.OrderServices.Any(os => os.ServiceId == serviceId2));

            serviceRepositoryMock.Verify(r => r.FindByIdAsync(serviceId1), Times.Once);
            serviceRepositoryMock.Verify(r => r.FindByIdAsync(serviceId2), Times.Once);
            orderRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public async Task FindByIdAsyncShouldReturnOrderGetDto()
        {
            // Arrange
            var orderId = order.Id;

            orderRepositoryMock
                .Setup(r => r.FindByIdAsync(orderId))
                .ReturnsAsync(order);

            mapperMock
                .Setup(m => m.Map<OrderGetDto>(order))
                .Returns(new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await orderService.FindByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(order.Id, result.Id);

            orderRepositoryMock.Verify(r => r.FindByIdAsync(orderId), Times.Once);
            mapperMock.Verify(m => m.Map<OrderGetDto>(order), Times.Once);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var orders = new List<Order>
            {
                order,
                order,
            };

            orderRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(orders);

            mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await orderService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);

            orderRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
            mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task GetListByUserIdAsyncShouldReturnListOfOrderGetDto()
        {
            // Arrange
            var userId = "1";
            var orders = new List<Order>
            {
                order,
                order,
            };

            orderRepositoryMock
                .Setup(r => r.GetByUserIdAsync(userId))
                .ReturnsAsync(orders);

            mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await orderService.GetListByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);


            orderRepositoryMock.Verify(r => r.GetByUserIdAsync(userId), Times.Once);
            mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task GetListByDatesAsyncShouldReturnListOfOrderGetDto()
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

            orderRepositoryMock
                .Setup(r => r.FindByDateAsync(beginDate, endDate, userId))
                .ReturnsAsync(orders);

            mapperMock
                .Setup(m => m.Map<OrderGetDto>(It.IsAny<Order>()))
                .Returns<Order>(order => new OrderGetDto
                {
                    Id = order.Id,
                });

            // Act
            var result = await orderService.GetListByDatesAsync(beginDate, endDate, userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(orders.Count, result.Count);


            orderRepositoryMock.Verify(r => r.FindByDateAsync(beginDate, endDate, userId), Times.Once);
            mapperMock.Verify(m => m.Map<OrderGetDto>(It.IsAny<Order>()), Times.Exactly(orders.Count));
        }

        [Test]
        public async Task RemoveAsyncShouldRemoveOrder()
        {
            var testOrder = order;
            orderRepositoryMock
                .Setup(r => r.FindByIdAsync(testOrder.Id))
                .ReturnsAsync(order);

            orderRepositoryMock
                .Setup(r => r.RemoveAsync(testOrder))
                .Returns(Task.CompletedTask);

            // Act
            await orderService.RemoveAsync(testOrder.Id);

            // Assert
            orderRepositoryMock.Verify(r => r.FindByIdAsync(testOrder.Id), Times.Once);
            orderRepositoryMock.Verify(r => r.RemoveAsync(testOrder), Times.Once);
        }
    }
}