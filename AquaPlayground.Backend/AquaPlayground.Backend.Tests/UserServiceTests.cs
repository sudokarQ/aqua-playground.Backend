namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            _userRepositoryMock = new Mock<IUserRepository>();

            _mapperMock = new Mock<IMapper>();

            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _userManagerMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfUserGetDto()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", Name = "John" },
                new User { Id = "2", Name = "Jane" }
            };

            var userGetDtos = new List<UserGetDto>
            {
                new UserGetDto { Id = "1", Name = "John" },
                new UserGetDto { Id = "2", Name = "Jane" }
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(users);

            _mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(users))
                .Returns(userGetDtos);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.AreEqual(userGetDtos, result);
        }

        [Test]
        public async Task FindByIdAsync_ReturnsUserGetDto()
        {
            // Arrange
            var userId = "1";
            var user = new User { Id = userId, Name = "John" };
            var userGetDto = new UserGetDto { Id = userId, Name = "John" };

            _userRepositoryMock.Setup(repo => repo.FindByIdAsync(userId))
                .ReturnsAsync(user);

            _mapperMock.Setup(mapper => mapper.Map<UserGetDto>(user))
                .Returns(userGetDto);

            // Act
            var result = await _userService.FindByIdAsync(userId);

            // Assert
            Assert.AreEqual(userGetDto, result);
        }

        [Test]
        public async Task FindByLoginAsync_ReturnsListOfUserGetDto()
        {
            // Arrange
            var login = "john123";
            var users = new List<User>
            {
                new User { Id = "1", Name = "John" },
                new User { Id = "2", Name = "Jane" }
            };

            var userGetDtos = new List<UserGetDto>
            {
                new UserGetDto { Id = "1", Name = "John" },
                new UserGetDto { Id = "2", Name = "Jane" }
            };

            _userRepositoryMock.Setup(repo => repo.FindByLoginAsync(login))
                .ReturnsAsync(users);

            _mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(users))
                .Returns(userGetDtos);

            // Act
            var result = await _userService.FindByLoginAsync(login);

            Assert.AreEqual(userGetDtos, result);
        }

        [Test]
        public async Task UpdateUserAsync_ReturnsSuccessResult_WhenUpdateSuccessful()
        {
            // Arrange
            var dto = new UserUpdateDto
            {
                Name = "John Doe",
                Surname = "Doe"
            };
            var user = new User { Id = "1", Email = "john@example.com" };

            _userManagerMock.Setup(manager => manager.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.UpdateUserAsync(dto, user);

            // Assert
            Assert.IsTrue(result.Succeeded);
        }

        //[Test]
        //public async Task UpdateUserAsync_ReturnsFailedResult_WhenEmailAlreadyTaken()
        //{
        //    // Arrange
        //    var user = new User { Id = "1", Email = "john@example.com" };

        //    _userRepositoryMock.Setup(repo => repo.AnyAsync(u => u.Email == dto.Email))
        //        .ReturnsAsync(true);

        //    // Act
        //    var result = await _userService.UpdateUserAsync(dto, user);

        //    // Assert
        //    Assert.IsFalse(result.Succeeded);
        //    Assert.AreEqual("Email is already taken", result.Errors.First().Description);
        //}

        [Test]
        public async Task DeleteUserAsync_ReturnsSuccessResult_WhenDeleteSuccessful()
        {
            // Arrange
            var user = new User { Id = "1" };

            _userManagerMock.Setup(manager => manager.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.DeleteUserAsync(user);

            // Assert
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task DeleteUserAsync_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            var user = new User { Id = "1" };

            _userRepositoryMock.Setup(repo => repo.FindByIdAsync(user.Id))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteUserAsync(user);

            // Assert
            Assert.IsNull(result);
        }
    }
}