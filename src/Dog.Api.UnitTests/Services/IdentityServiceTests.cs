using Dog.Api.Core.Errors;
using Dog.Api.TestFramework.Fixtures;

namespace Dog.Api.UnitTests.Services;

public class IdentityServiceTests : IClassFixture<ModelFakerFixture>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly GenerateToken.IdentityService _service;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly ModelFakerFixture _modelFakerFixture;
    private readonly Mock<ILogger<GenerateToken.IdentityService>> _logger;

    public IdentityServiceTests(ModelFakerFixture modelFakerFixture) 
    {
        var jwtOptions = new JwtOptions() 
        {
            Audience = TestConstants.Jwt.Audience,
            Issuer = TestConstants.Jwt.Issuer,
            ExpiryTime = TestConstants.Jwt.ExpiryTime,
            SigningKey = TestConstants.Jwt.SigningKey,
        };
        
        _modelFakerFixture = modelFakerFixture;
        _logger = new Mock<ILogger<GenerateToken.IdentityService>>();
        _userManagerMock = UserManagerMock.MockUserManager<User>();
		_userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<User>());
		_userManagerMock.Setup(x => x.FindByEmailAsync(Constants.User.Email)).ReturnsAsync(_modelFakerFixture.User.Generate());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>() { "User" });
        _options = Options.Create(jwtOptions);
        _service = new GenerateToken.IdentityService(_options, _logger.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task IdentityService_ShouldPass() 
    {
        var result = await _service.GenerateAuthToken(Constants.User.Email, Constants.User.Password);

        result.Should().NotBeNull();
    }

	[Fact]
	public async Task IdentityService_ShouldThrow()
	{
		await _service.Invoking(x => x.GenerateAuthToken(string.Empty, string.Empty)).Should().ThrowAsync<ErrorResult>();
	}
}

