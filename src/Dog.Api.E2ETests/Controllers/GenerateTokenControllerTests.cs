namespace Dog.Api.IntegrationTests.Controllers;

public class GenerateTokenControllerTests : BaseTest, IClassFixture<SharedFixture>
{
    public GenerateTokenControllerTests ( SharedFixture fixture ) : base(fixture.TestServerFixture) { }

    [Fact]
    public async Task GenerateToken_ShouldPassWithValidCredentials ()
    {
        var request = await Client.PostAsJsonAsync("/GenerateToken", new TokenRequest { Email = Constants.AdminUser.Email, Password = Constants.AdminUser.Password });

        request.Should().NotBeNull();
    }

    //[Fact]
    //public async Task GenerateToken_ShouldFailWithInvalidCredentials ()
    //{
    //    var request = await Client.PostAsJsonAsync("/GenerateToken", new TokenRequest { Email = string.Empty, Password = string.Empty });
    //    var response = await request.Content.ReadAsStringAsync();

    //    response.Should().Be(Constants.ErrorMessages.InvalidCredentials);
    //}
}

