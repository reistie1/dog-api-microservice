namespace Dog.Api.IntegrationTests.Services;

public class FeatureFlagServiceTests 
{
    public FeatureFlagServiceTests() 
    {   
    }

    [Fact]
    public async Task FeatureFlagService_ShouldPass() 
    {
		var splitOptions = new SplitOptions {
			ApiKey = TestConstants.Split.ApiKey,
			TreatmentName = TestConstants.Split.TreatmentName,
			UserId = TestConstants.Split.UserId,
			WaitTime = TestConstants.Split.WaitTime,
		};
		var options = Options.Create(splitOptions);

		var service = new FeatureFlagController.FeatureFlagService(options);
        var response = await service.CheckFeatureFlag();

        response.Should().Be(true);
    }

	[Fact]
	public async Task FeatureFlagService_ShouldFail()
	{
		var splitOptions = new SplitOptions {
			ApiKey = TestConstants.Split.ApiKey,
			TreatmentName = string.Empty,
			UserId = TestConstants.Split.UserId,
			WaitTime = TestConstants.Split.WaitTime,
		};
		var options = Options.Create(splitOptions);

		var service = new FeatureFlagController.FeatureFlagService(options);
		var response = await service.CheckFeatureFlag();

		response.Should().Be(false);
	}
}

