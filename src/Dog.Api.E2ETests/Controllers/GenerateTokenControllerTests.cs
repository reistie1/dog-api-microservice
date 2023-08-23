namespace Dog.Api.E2ETests.Controllers;

[CollectionDefinition("GenerateTokenCollection", DisableParallelization = true)]
[Collection("GenerateTokenCollection")]
public class GenerateTokenControllerFailureTest : BaseTest, IClassFixture<SharedFixture>
{
	public GenerateTokenControllerFailureTest(SharedFixture fixture) : base(fixture.TestServerFixture) { }

	[Fact]
	public async Task GenerateToken_ShouldFailWithInvalidCredentials()
	{
		var request = await Client.PostAsJsonAsync("/GenerateToken", new GenerateTokenController.TokenRequestCommand { Email = string.Empty, Password = string.Empty });
		
		request.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
	}

	[Fact]
	public async Task GenerateToken_ShouldPassWithValidCredentials()
	{
		var request = await Client.PostAsJsonAsync("/GenerateToken", new GenerateTokenController.TokenRequestCommand { Email = Constants.AdminUser.Email, Password = Constants.AdminUser.Password });

		request.Should().NotBeNull();
	}
}
