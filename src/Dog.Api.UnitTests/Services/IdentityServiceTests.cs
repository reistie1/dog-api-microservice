namespace Dog.Api.UnitTests.Services;

public class IdentityServiceTests : IClassFixture<ModelFakerFixture>
{
	private readonly Mock<ILogger<IdentityService>> _logger;
	private readonly TokenRequestCommand _model;
	private readonly ModelFakerFixture _modelFakerFixture;
	private readonly IOptions<JwtOptions> _options;
	private readonly Mock<IPasswordHasher> _passwordHasher;
	private readonly IdentityService _service;
	private readonly Mock<UserManager<User>> _userManagerMock;

	public IdentityServiceTests(ModelFakerFixture modelFakerFixture) 
    {
        var jwtOptions = (JwtOptions)modelFakerFixture.JwtOptions;
        
        _modelFakerFixture = modelFakerFixture;
		_model = _modelFakerFixture.TokenRequestCommand.Generate();
		_passwordHasher = new Mock<IPasswordHasher>();
        _logger = new Mock<ILogger<IdentityService>>();
        _userManagerMock = UserManagerMock.MockUserManager<User>();
		_userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<User>());
		_userManagerMock.Setup(x => x.FindByEmailAsync(Constants.AdminUser.Email)).ReturnsAsync(_modelFakerFixture.User.Generate());
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>() { "User" });
		_passwordHasher.Setup(x => x.VerifyPassword(It.IsAny<User>(), It.IsAny<string>())).Returns(true);
        _options = Options.Create(jwtOptions);
        _service = new IdentityService(_options, _logger.Object, _passwordHasher.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task IdentityService_ShouldPass() 
    {
        var result = await _service.GenerateAuthTokenAsync(_model);

        result.Should().NotBeNull();
    }

	[Fact]
	public async Task IdentityService_ShouldThrow()
	{
		var model = _modelFakerFixture.TokenRequestCommand.Generate();
		model.Email = string.Empty;
		model.Password = string.Empty;
		await _service.Invoking(x => x.GenerateAuthTokenAsync(model)).Should().ThrowAsync<ErrorResult>();
	}
}
