namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class PromotionServiceTests
    {
        private IPromotionService promotionService;
        
        private Mock<IPromotionRepository> promotionRepositoryMock;
        
        private Mock<IMapper> mapperMock;

        public static readonly PromotionPostDto ValidDto = new PromotionPostDto
        {
            Name = "Valid Promotion",
            Description = "This is a valid promotion",
            DiscountPercent = 10,
            BeginDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(7),
            ServiceId = Guid.NewGuid()
        };

        public static readonly PromotionPostDto InvalidDtoInvalidDiscount = new PromotionPostDto
        {
            Name = "Invalid Discount Promotion",
            Description = "This promotion has an invalid discount",
            DiscountPercent = 150,
            BeginDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(7),
            ServiceId = Guid.NewGuid()
        };

        public static readonly Promotion validPromotion = new Promotion
        {
            Id = Guid.NewGuid(),
            Name = "Valid Promotion",
            Description = "This is a valid promotion",
            DiscountPercent = 10,
            BeginDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(7),
            ServiceId = Guid.NewGuid()
        };

        [SetUp]
        public void Setup()
        {
            promotionRepositoryMock = new Mock<IPromotionRepository>();
            mapperMock = new Mock<IMapper>();
            promotionService = new PromotionService(promotionRepositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public async Task CreateAsyncValidPromotionCallsRepositoryCreateAsync()
        {
            // Arrange
            var promotion = ValidDto;
            mapperMock
                .Setup(m => m.Map<Promotion>(It.IsAny<PromotionPostDto>()))
                .Returns(new Promotion());

            // Act
            await promotionService.CreateAsync(promotion);

            // Assert
            promotionRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Promotion>()), Times.Once);
        }

        [Test]
        public void CreateAsyncInvalidPromotionThrowsValidationException()
        {
            // Arrange
            var promotion = InvalidDtoInvalidDiscount;

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => promotionService.CreateAsync(promotion));
        }

        [Test]
        public async Task GetAllAsyncReturnsListOfPromotionGetDto()
        {
            // Arrange
            var promotions = new List<Promotion> { validPromotion, validPromotion};
            promotionRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(promotions);
            mapperMock
                .Setup(m => m.Map<List<PromotionGetDto>>(promotions))
                .Returns(new List<PromotionGetDto>());

            // Act
            var result = await promotionService.GetAllAsync();

            // Assert
            Assert.IsInstanceOf<List<PromotionGetDto>>(result);
        }

        [Test]
        public async Task GetListByNameAsyncReturnsListOfPromotionGetDto()
        {
            // Arrange
            var name = "promotion name";
            var promotions = new List<Promotion> { validPromotion, validPromotion };
            promotionRepositoryMock
                .Setup(r => r.GetListByNameAsync(name))
                .ReturnsAsync(promotions);
            mapperMock
                .Setup(m => m.Map<List<PromotionGetDto>>(promotions))
                .Returns(new List<PromotionGetDto>());

            // Act
            var result = await promotionService.GetListByNameAsync(name);

            // Assert
            Assert.IsInstanceOf<List<PromotionGetDto>>(result);
        }

        [Test]
        public async Task FindByIdAsyncReturnsPromotionGetDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var promotion = new Promotion();
            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync(promotion);
            mapperMock
                .Setup(m => m.Map<PromotionGetDto>(promotion))
                .Returns(new PromotionGetDto());

            // Act
            var result = await promotionService.FindByIdAsync(id);

            // Assert
            Assert.IsInstanceOf<PromotionGetDto>(result);
        }

        [Test]
        public void RemoveAsyncPromotionNotExistsThrowsArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync((Promotion)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => promotionService.RemoveAsync(id));
        }

        [Test]
        public async Task UpdateAsyncValidPromotionUpdatesPromotion()
        {
            // Arrange
            var promotionId = Guid.NewGuid();
            var dto = new PromotionPutDto
            {
                Id = promotionId,
                Name = "Updated Promotion",
                Description = "Updated description",
                DiscountPercent = 50,
                BeginDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(7)
            };

            var existingPromotion = new Promotion
            {
                Id = promotionId,
                Name = "Old Promotion",
                Description = "Old description",
                DiscountPercent = 20,
                BeginDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(10)
            };

            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync(existingPromotion);

            // Act
            await promotionService.UpdateAsync(dto);

            // Assert
            promotionRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Promotion>(p =>
                p.Id == promotionId &&
                p.Name == dto.Name &&
                p.Description == dto.Description &&
                p.DiscountPercent == dto.DiscountPercent &&
                p.BeginDate == dto.BeginDate &&
                p.EndDate == dto.EndDate
            )), Times.Once);
        }

        [Test]
        public void UpdateAsyncPromotionNotExistsThrowsArgumentException()
        {
            // Arrange
            var promotionId = Guid.NewGuid();
            var dto = new PromotionPutDto { Id = promotionId };

            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync((Promotion)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => promotionService.UpdateAsync(dto));
        }

        [Test]
        public async Task RemoveAsyncPromotionExistsCallsRepositoryRemoveAsync()
        {
            // Arrange
            var id = Guid.NewGuid();
            var promotion = new Promotion();
            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync(promotion);

            // Act
            await promotionService.RemoveAsync(id);

            // Assert
            promotionRepositoryMock.Verify(r => r.RemoveAsync(promotion), Times.Once);
        }

        [Test]
        public void UpdateAsyncInvalidPromotionThrowsValidationException()
        {
            // Arrange
            var promotionId = Guid.NewGuid();
            var dto = new PromotionPutDto
            {
                Id = promotionId,
                Name = "Invalid Promotion",
                Description = "Invalid description",
                DiscountPercent = 120,
                BeginDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(1)
            };

            var existingPromotion = new Promotion
            {
                Id = promotionId,
                Name = "Old Promotion",
                Description = "Old description",
                DiscountPercent = 20,
                BeginDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(10)
            };

            promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync(existingPromotion);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => promotionService.UpdateAsync(dto));
        }
    }
}