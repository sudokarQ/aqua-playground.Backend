namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class PromotionServiceTests
    {
        private IPromotionService _promotionService;
        
        private Mock<IPromotionRepository> _promotionRepositoryMock;
        
        private Mock<IMapper> _mapperMock;

        public static readonly PromotionPostDto ValidDto = new PromotionPostDto
        {
            Name = "Valid Promotion",
            Description = "This is a valid promotion",
            DiscountPercent = 10,
            BeginDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(7),
            ServiceId = Guid.NewGuid()
        };

        public static readonly PromotionPostDto InvalidDto_InvalidDiscount = new PromotionPostDto
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
            _promotionRepositoryMock = new Mock<IPromotionRepository>();
            _mapperMock = new Mock<IMapper>();
            _promotionService = new PromotionService(_promotionRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateAsync_ValidPromotion_CallsRepositoryCreateAsync()
        {
            // Arrange
            var promotion = ValidDto;
            _mapperMock
                .Setup(m => m.Map<Promotion>(It.IsAny<PromotionPostDto>()))
                .Returns(new Promotion());

            // Act
            await _promotionService.CreateAsync(promotion);

            // Assert
            _promotionRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Promotion>()), Times.Once);
        }

        [Test]
        public void CreateAsync_InvalidPromotion_ThrowsValidationException()
        {
            // Arrange
            var promotion = InvalidDto_InvalidDiscount;

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _promotionService.CreateAsync(promotion));
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfPromotionGetDto()
        {
            // Arrange
            var promotions = new List<Promotion> { validPromotion, validPromotion};
            _promotionRepositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(promotions);
            _mapperMock
                .Setup(m => m.Map<List<PromotionGetDto>>(promotions))
                .Returns(new List<PromotionGetDto>());

            // Act
            var result = await _promotionService.GetAllAsync();

            // Assert
            Assert.IsInstanceOf<List<PromotionGetDto>>(result);
        }

        [Test]
        public async Task GetListByNameAsync_ReturnsListOfPromotionGetDto()
        {
            // Arrange
            var name = "promotion name";
            var promotions = new List<Promotion> { validPromotion, validPromotion };
            _promotionRepositoryMock
                .Setup(r => r.GetListByNameAsync(name))
                .ReturnsAsync(promotions);
            _mapperMock
                .Setup(m => m.Map<List<PromotionGetDto>>(promotions))
                .Returns(new List<PromotionGetDto>());

            // Act
            var result = await _promotionService.GetListByNameAsync(name);

            // Assert
            Assert.IsInstanceOf<List<PromotionGetDto>>(result);
        }

        [Test]
        public async Task FindByIdAsync_ReturnsPromotionGetDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var promotion = new Promotion();
            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync(promotion);
            _mapperMock
                .Setup(m => m.Map<PromotionGetDto>(promotion))
                .Returns(new PromotionGetDto());

            // Act
            var result = await _promotionService.FindByIdAsync(id);

            // Assert
            Assert.IsInstanceOf<PromotionGetDto>(result);
        }

        [Test]
        public void RemoveAsync_PromotionNotExists_ThrowsArgumentNullException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync((Promotion)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _promotionService.RemoveAsync(id));
        }

        [Test]
        public async Task UpdateAsync_ValidPromotion_UpdatesPromotion()
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

            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync(existingPromotion);

            // Act
            await _promotionService.UpdateAsync(dto);

            // Assert
            _promotionRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Promotion>(p =>
                p.Id == promotionId &&
                p.Name == dto.Name &&
                p.Description == dto.Description &&
                p.DiscountPercent == dto.DiscountPercent &&
                p.BeginDate == dto.BeginDate &&
                p.EndDate == dto.EndDate
            )), Times.Once);
        }

        [Test]
        public void UpdateAsync_PromotionNotExists_ThrowsArgumentException()
        {
            // Arrange
            var promotionId = Guid.NewGuid();
            var dto = new PromotionPutDto { Id = promotionId };

            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync((Promotion)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _promotionService.UpdateAsync(dto));
        }

        [Test]
        public async Task RemoveAsync_PromotionExists_CallsRepositoryRemoveAsync()
        {
            // Arrange
            var id = Guid.NewGuid();
            var promotion = new Promotion();
            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(id))
                .ReturnsAsync(promotion);

            // Act
            await _promotionService.RemoveAsync(id);

            // Assert
            _promotionRepositoryMock.Verify(r => r.RemoveAsync(promotion), Times.Once);
        }

        [Test]
        public void UpdateAsync_InvalidPromotion_ThrowsValidationException()
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

            _promotionRepositoryMock
                .Setup(r => r.FindByIdAsync(promotionId))
                .ReturnsAsync(existingPromotion);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _promotionService.UpdateAsync(dto));
        }
    }
}