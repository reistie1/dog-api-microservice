namespace Dog.Api.E2ETests.Controllers;


[CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
public class NonParallelCollectionDefinitionClass
{
}

[Collection("Non-Parallel Collection")]
public class GenerateTokenControllerFailureTest : BaseTest, IClassFixture<SharedFixture>
{
	public GenerateTokenControllerFailureTest(SharedFixture fixture) : base(fixture.TestServerFixture) { }

	[Fact]
	public async Task GenerateToken_ShouldFailWithInvalidCredentials()
	{
		await Client.Invoking(x => x.PostAsJsonAsync("/GenerateToken", new GenerateToken.TokenRequest { Email = string.Empty, Password = string.Empty })).Should().ThrowAsync<ErrorResult>();
	}

	[Fact]
	public async Task GenerateToken_ShouldPassWithValidCredentials()
	{
		var request = await Client.PostAsJsonAsync("/GenerateToken", new GenerateToken.TokenRequest { Email = Constants.AdminUser.Email, Password = Constants.AdminUser.Password });

		request.Should().NotBeNull();
	}
}

