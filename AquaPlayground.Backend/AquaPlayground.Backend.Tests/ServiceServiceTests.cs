﻿using AquaPlayground.Backend.BuisnessLayer.Intefaces;
using AquaPlayground.Backend.Common.Models.Dto;
using AquaPlayground.Backend.Common.Models.Dto.Service;
using AquaPlayground.Backend.DataLayer.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AquaPlayground.Backend.BuisnessLayer.Services.Tests
{
    [TestFixture]
    public class ServiceServiceTests
    {
        private IServiceService _serviceService;
        private Mock<IServiceRepository> _serviceRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _serviceService = new ServiceService(_serviceRepositoryMock.Object);
        }

        [Test]
        public async Task CreateAsync_ValidInput_CreatesService()
        {
            // Arrange
            var service = new ServicePostDto
            {
                Name = "Service",
                TypeService = "Type",
                Price = 10,
                Description = "Description"
            };

            _serviceRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<Service>()))
     .Returns(Task.FromResult(new Service { Id = new Guid() }));


            // Act
            await _serviceService.CreateAsync(service);

            // Assert
            _serviceRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Service>()), Times.Once);
        }

        [Test]
        public void CreateAsync_InvalidInput_ThrowsException()
        {
            // Arrange
            var service = new ServicePostDto();

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _serviceService.CreateAsync(service));
        }

        [Test]
        public async Task FindByIdAsync_ServiceExists_ReturnsService()
        {
            // Arrange
            var serviceId = new Guid();

            _serviceRepositoryMock.Setup(x => x.FindByIdAsync(serviceId))
                .ReturnsAsync(new Service { Id = serviceId });

            // Act
            var result = await _serviceService.FindByIdAsync(new IdDto { Id = serviceId });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(serviceId, result.First().Id);
        }

        [Test]
        public async Task FindByIdAsync_ServiceDoesNotExist_ReturnsNull()
        {
            // Arrange
            var serviceId = new Guid();

            _serviceRepositoryMock.Setup(x => x.FindByIdAsync(serviceId))
                .ReturnsAsync((Service)null);

            // Act
            var result = await _serviceService.FindByIdAsync(new IdDto { Id = serviceId });

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetListByNameAsync_ServicesExist_ReturnsServices()
        {
            // Arrange
            var serviceName = "Service";

            _serviceRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Service, bool>>>()))
                .ReturnsAsync(new List<Service>
                {
                    new Service { Name = serviceName + "1" },
                    new Service { Name = serviceName + "2" },
                    new Service { Name = serviceName + "3" }
                });

            // Act
            var result = await _serviceService.GetListByNameAsync(serviceName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.All(x => x.Name.StartsWith(serviceName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}