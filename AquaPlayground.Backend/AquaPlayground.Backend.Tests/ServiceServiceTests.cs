namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class ServiceServiceTests
    {
        private IServiceService serviceService;

        private Mock<IServiceRepository> serviceRepositoryMock;

        [SetUp]
        public void Setup()
        {
            serviceRepositoryMock = new Mock<IServiceRepository>();
            serviceService = new ServiceService(serviceRepositoryMock.Object);
        }

        [Test]
        public async Task CreateAsyncValidInputCreatesService()
        {
            // Arrange
            var service = new ServicePostDto
            {
                Name = "Service",
                TypeService = "Type",
                Price = 10,
                Description = "Description"
            };

            serviceRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Service>()))
     .Returns(Task.FromResult(new Service { Id = new Guid() }));


            // Act
            await serviceService.CreateAsync(service);

            // Assert
            serviceRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Service>()), Times.Once);
        }

        [Test]
        public async Task FindByIdAsyncServiceExistsReturnsService()
        {
            // Arrange
            var serviceId = new Guid();

            serviceRepositoryMock.Setup(x => x.FindByIdAsync(serviceId))
                .ReturnsAsync(new Service { Id = serviceId });

            // Act
            var result = await serviceService.FindByIdAsync(serviceId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(serviceId, result.First().Id);
        }

        [Test]
        public async Task FindByIdAsyncServiceDoesNotExistReturnsNull()
        {
            // Arrange
            var serviceId = new Guid();

            serviceRepositoryMock.Setup(x => x.FindByIdAsync(serviceId))
                .ReturnsAsync((Service)null);

            // Act
            var result = await serviceService.FindByIdAsync(serviceId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetListByNameAsyncServicesExistReturnsServices()
        {
            // Arrange
            var serviceName = "Service";

            serviceRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                .ReturnsAsync(new List<Service>
                {
                    new Service { Name = serviceName + "1" },
                    new Service { Name = serviceName + "2" },
                    new Service { Name = serviceName + "3" }
                });

            // Act
            var result = await serviceService.GetListByNameAsync(serviceName);

            // Assert
            Assert.IsNotNull(result);

            Assert.AreEqual(3, result.Count);
            
            Assert.IsTrue(result.All(x => x.Name.StartsWith(serviceName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}