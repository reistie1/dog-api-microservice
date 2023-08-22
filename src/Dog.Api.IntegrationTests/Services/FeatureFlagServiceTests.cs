namespace Dog.Api.IntegrationTests.Services;

public class FeatureFlagServiceTests : IClassFixture<SharedFixture>
{
	private readonly SharedFixture _sharedFixture;

	public FeatureFlagServiceTests(SharedFixture sharedFixture) 
    {
		_sharedFixture = sharedFixture;
		sharedFixture.OptionsFixture.CreateSplitOptions();
    }

    [Fact]
    public async Task FeatureFlagService_ShouldPass() 
    {
		var options = _sharedFixture.OptionsFixture.splitOptions;
		var service = new FeatureFlagController.FeatureFlagService(options);
        var response = await service.CheckFeatureFlag();

        response.Should().Be(true);
    }

	[Fact]
	public async Task FeatureFlagService_ShouldFail()
	{
		var splitOptions = (SplitOptions)_sharedFixture.ModelFakerFixture.SplitOptions;
		var options = Options.Create(splitOptions);
		var service = new FeatureFlagController.FeatureFlagService(options);
		var response = await service.CheckFeatureFlag();

		response.Should().Be(false);
	}
}
