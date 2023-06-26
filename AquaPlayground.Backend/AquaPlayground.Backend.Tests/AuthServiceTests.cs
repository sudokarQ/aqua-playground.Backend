namespace AquaPlayground.Backend.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<UserManager<User>> userManagerMock;
        
        private Mock<IConfiguration> configurationMock;
        
        private AuthService authService;

        [SetUp]
        public void Setup()
        {
            userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            configurationMock = new Mock<IConfiguration>();
            authService = new AuthService(userManagerMock.Object, configurationMock.Object);
        }

        [Test]
        public async Task ValidateUserReturnsTrueForValidUser()
        {
            // Arrange
            var user = new User { UserName = "test@example.com" };

            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await authService.ValidateUser(new UserLoginDto { Email = "test@example.com", Password = "password" });

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ValidateUserReturnsFalseForInvalidUser()
        {
            // Arrange
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await authService.ValidateUser(new UserLoginDto { Email = "test@example.com", Password = "password" });

            // Assert
            Assert.IsFalse(result);
        }
    }
}