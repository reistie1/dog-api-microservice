namespace Dog.Api.UnitTests.Handlers;

public class RandomBreedCommandHandlerTests : IClassFixture<SharedFixture>
	{
	private readonly Response<string> _result;
	private readonly Mock<IRandomBreedImageService> _randomBreedImageServiceMock;

	public RandomBreedCommandHandlerTests(SharedFixture sharedFixture)
	{
		_result = sharedFixture.ModelFakerFixture.ResponseString.Generate();
		_randomBreedImageServiceMock = new Mock<IRandomBreedImageService>();
		_randomBreedImageServiceMock.Setup(x => x.RandomBreedImage()).ReturnsAsync(_result);
	}

	[Fact]
	public async void RandomBreedCommandHandler_ShouldPass()
	{
		var command = new RandomBreedCommand();
		var handler = new RandomBreedCommandHandler(_randomBreedImageServiceMock.Object);
		var result = await handler.Handle(command, CancellationToken.None);

		result.Should().NotBeNull();
		result.Should().Be(_result);
	}
}

