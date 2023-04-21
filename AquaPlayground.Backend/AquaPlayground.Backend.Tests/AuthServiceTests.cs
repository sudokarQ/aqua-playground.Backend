[TestFixture]
public class AuthServiceTests
{
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<IConfiguration> _configurationMock;
    private AuthService _authService;

    [SetUp]
    public void Setup()
    {
        _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();
        _authService = new AuthService(_userManagerMock.Object, _configurationMock.Object);
    }

    [Test]
    public async Task ValidateUser_Returns_True_For_Valid_User()
    {
        // Arrange
        var user = new User { UserName = "test@example.com" };
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _authService.ValidateUser(new UserLoginDto { Email = "test@example.com", Password = "password" });

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task ValidateUser_Returns_False_For_Invalid_User()
    {
        // Arrange
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        // Act
        var result = await _authService.ValidateUser(new UserLoginDto { Email = "test@example.com", Password = "password" });

        // Assert
        Assert.IsFalse(result);
    }
}