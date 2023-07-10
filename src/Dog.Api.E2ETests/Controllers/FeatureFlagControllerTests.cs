namespace Dog.Api.IntegrationTests.Controllers;

public class FeatureFlagControllerTests : BaseTest, IClassFixture<SharedFixture>
{
    public FeatureFlagControllerTests ( SharedFixture sharedFixture ) : base(sharedFixture.TestServerFixture)
    {}

    [Fact]
    public async Task FeatureFlag_ShouldPass()
    {
        var response = await ApiClient.GetFeatureFlagStatusAsync();

        response.Should().NotBeNull();
        response.Should().Match("Feature Flag is active");
    }
}

