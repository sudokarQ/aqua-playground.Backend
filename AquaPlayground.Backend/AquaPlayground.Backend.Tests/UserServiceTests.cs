namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService userService;
        
        private Mock<IUserRepository> userRepositoryMock;
        
        private Mock<UserManager<User>> userManagerMock;
        
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            userRepositoryMock = new Mock<IUserRepository>();

            mapperMock = new Mock<IMapper>();

            userService = new UserService(userRepositoryMock.Object, mapperMock.Object, userManagerMock.Object);
        }

        [Test]
        public async Task GetAllAsyncReturnsListOfUserGetDto()
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

            userRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(users);

            mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(users))
                .Returns(userGetDtos);

            // Act
            var result = await userService.GetAllAsync();

            // Assert
            Assert.AreEqual(userGetDtos, result);
        }

        [Test]
        public async Task FindByIdAsyncReturnsUserGetDto()
        {
            // Arrange
            var userId = "1";
            var user = new User { Id = userId, Name = "John" };
            var userGetDto = new UserGetDto { Id = userId, Name = "John" };

            userRepositoryMock.Setup(repo => repo.FindByIdAsync(userId))
                .ReturnsAsync(user);

            mapperMock.Setup(mapper => mapper.Map<UserGetDto>(user))
                .Returns(userGetDto);

            // Act
            var result = await userService.FindByIdAsync(userId);

            // Assert
            Assert.AreEqual(userGetDto, result);
        }

        [Test]
        public async Task FindByLoginAsyncReturnsListOfUserGetDto()
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

            userRepositoryMock.Setup(repo => repo.FindByLoginAsync(login))
                .ReturnsAsync(users);

            mapperMock.Setup(mapper => mapper.Map<List<UserGetDto>>(users))
                .Returns(userGetDtos);

            // Act
            var result = await userService.FindByLoginAsync(login);

            Assert.AreEqual(userGetDtos, result);
        }

        [Test]
        public async Task UpdateUserAsyncReturnsSuccessResultWhenUpdateSuccessful()
        {
            // Arrange
            var dto = new UserUpdateDto
            {
                Name = "John Doe",
                Surname = "Doe"
            };
            var user = new User { Id = "1", Email = "john@example.com" };

            userManagerMock.Setup(manager => manager.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.UpdateUserAsync(dto, user);

            // Assert
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task DeleteUserAsyncReturnsSuccessResultWhenDeleteSuccessful()
        {
            // Arrange
            var user = new User { Id = "1" };

            userManagerMock.Setup(manager => manager.DeleteAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await userService.DeleteUserAsync(user);

            // Assert
            Assert.IsTrue(result.Succeeded);
        }

        [Test]
        public async Task DeleteUserAsyncReturnsNullWhenUserNotFound()
        {
            // Arrange
            var user = new User { Id = "1" };

            userRepositoryMock.Setup(repo => repo.FindByIdAsync(user.Id))
                .ReturnsAsync((User)null);

            // Act
            var result = await userService.DeleteUserAsync(user);

            // Assert
            Assert.IsNull(result);
        }
    }
}