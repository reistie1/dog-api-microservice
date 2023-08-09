namespace Dog.Api.UnitTests.Helpers;

public class DatabaseHelperTests : IClassFixture<SharedFixture>
{
	private readonly SharedFixture _sharedFixture;
	private readonly ILogger<DatabaseHelper> _logger;
	private readonly Mock<IPasswordHasher> _passwordHasherMock;

	public DatabaseHelperTests(SharedFixture sharedFixture)
	{
		_sharedFixture = sharedFixture;
		_logger = new LoggerFactory().CreateLogger<DatabaseHelper>();
		_passwordHasherMock = new Mock<IPasswordHasher>();
		_passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Throws(new Exception("Failed", new Exception("Invalid user")));
	}

	[Fact]
	public void CreateDatabase_ShouldPass()
	{
		var identityContext = _sharedFixture.DatabaseFixture.CreateIdentityContext();
		new DatabaseHelper(identityContext, _logger, new PasswordHasher()).CreateDatabase();

		identityContext.Users.Should().NotBeNull();
		identityContext.Roles.Should().NotBeNull();
	}

	[Fact]
	public void CreateDatabase_ShouldFail()
	{
		var identityContext = _sharedFixture.DatabaseFixture.CreateIdentityContext();
		var helper = new DatabaseHelper(new IdentityContext(new DbContextOptions<IdentityContext>()), _logger, new PasswordHasher());

		helper.Invoking(x => x.CreateDatabase()).Should().Throw<ErrorResult>();
	}

	[Fact]
	public async void SeedDatabase_ShouldPass()
	{
		var identityContext = _sharedFixture.DatabaseFixture.CreateIdentityContext();
		var databaseHelper = new DatabaseHelper(identityContext, _logger, new PasswordHasher());
		databaseHelper.CreateDatabase();
		await databaseHelper.SeedData();

		identityContext.Roles.Should().Contain(x => x.Name == Constants.AdminRole.Admin);
		identityContext.Users.Should().Contain(x => x.Email == Constants.AdminUser.Email);
	}

	[Fact]
	public async void SeedDatabase_ShouldFail()
	{
		var identityContext = _sharedFixture.DatabaseFixture.CreateIdentityContext();
		var databaseHelper = new DatabaseHelper(identityContext, _logger, _passwordHasherMock.Object);

		await databaseHelper.Invoking(x => x.SeedData()).Should().ThrowAsync<Exception>();	
	}
}

