namespace Dog.Api.TestFramework.Fixtures;

public class DatabaseFixture
{
	private IdentityContext _identityContext { get; set; } = default!;

	public DatabaseFixture()
	{
		_identityContext = CreateIdentityContext();
	}

	public IdentityContext CreateIdentityContext()
	{
		var dbOptions = new DbContextOptionsBuilder<IdentityContext>().UseInMemoryDatabase("testDb");

		return new IdentityContext(dbOptions.Options);
	}
}

