namespace Dog.Api.UnitTests.Helpers;

public class PasswordHasherTests : IClassFixture<SharedFixture>
{
	private readonly SharedFixture _sharedFixture;

	public PasswordHasherTests(SharedFixture sharedFixture) 
	{
		_sharedFixture = sharedFixture;
	}

	[Fact]
	public void HashPasword_ShouldPass()
	{
		var user = _sharedFixture.ModelFaker.User.Generate();
		var result = _sharedFixture.PasswordHasher.HashPassword(user, "Password1!");

		result.Should().NotBeNull();
		user.PasswordSalt.Should().NotBeNull();
	}

	[Fact]
	public void HashPasword_ShouldFail()
	{
		_sharedFixture.Invoking(x => x.PasswordHasher.HashPassword(null, "Password1!")).Should().Throw<ErrorResult>().WithMessage("User cannot be null");
	}

	[Fact]
	public void VerifyPasword_ShouldPass()
	{
		var user = _sharedFixture.ModelFaker.User.Generate();
		var hashResult = _sharedFixture.PasswordHasher.HashPassword(user, "Password1!");
		user.PasswordHash = hashResult;
		var result = _sharedFixture.PasswordHasher.VerifyPassword(user, "Password1!");

		result.Should().BeTrue();
	}

	[Fact]
	public void VerifyPasword_ShouldFail()
	{
		var user = _sharedFixture.ModelFaker.User.Generate();
		var hashResult = _sharedFixture.PasswordHasher.HashPassword(user, "Password1!");
		var result = _sharedFixture.PasswordHasher.VerifyPassword(user, "Password1!");

		result.Should().BeFalse();
	}
}

