namespace Dog.Api.UnitTests.Helpers;

public class DatabaseHelperTests : IClassFixture<SharedFixture>
{
	private readonly SharedFixture _sharedFixture;
	private readonly ILogger<DatabaseHelper> _logger;

	public DatabaseHelperTests(SharedFixture sharedFixture)
	{
		_sharedFixture = sharedFixture;
		_logger = new LoggerFactory().CreateLogger<DatabaseHelper>();
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
	public void SeedDatabase_ShouldPass()
	{
		var identityContext = _sharedFixture.DatabaseFixture.CreateIdentityContext();
		var databaseHelper = new DatabaseHelper(identityContext, _logger, new PasswordHasher());
		databaseHelper.CreateDatabase();
		databaseHelper.SeedData();

		identityContext.Roles.Should().Contain(x => x.Name == Constants.AdminRole.Admin);
		identityContext.Users.Should().Contain(x => x.Email == Constants.AdminUser.Email);

	}
}

