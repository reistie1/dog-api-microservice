namespace Dog.Api.UnitTests.Handlers;

public class FeatureFlagHandlerTests
{
	private readonly Mock<FeatureFlagController.IFeatureFlagService> _featureFlagServiceMock;

	public FeatureFlagHandlerTests()
	{
		_featureFlagServiceMock = new Mock<FeatureFlagController.IFeatureFlagService>();
		_featureFlagServiceMock.Setup(x => x.CheckFeatureFlag()).ReturnsAsync(true);
	}

	[Fact]
	public async void FeatureFlag_ShouldReturnTrue()
	{
		var handler = new FeatureFlagController.FeatureFlagCommandHandler(_featureFlagServiceMock.Object);
		var result = await handler.Handle(new FeatureFlagController.FeatureFlagCommand(), CancellationToken.None);

		result.Should().BeTrue();
	}

	[Fact]
	public async void FeatureFlag_ShouldReturnFalse()
	{
		_featureFlagServiceMock.Setup(x => x.CheckFeatureFlag()).ReturnsAsync(false);
		var handler = new FeatureFlagController.FeatureFlagCommandHandler(_featureFlagServiceMock.Object);
		var result = await handler.Handle(new FeatureFlagController.FeatureFlagCommand(), CancellationToken.None);

		result.Should().BeFalse();
	}
}

