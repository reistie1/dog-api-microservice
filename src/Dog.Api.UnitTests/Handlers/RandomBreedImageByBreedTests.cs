namespace Dog.Api.UnitTests.Handlers;

public class RandomBreedImageByBreedTests : IClassFixture<SharedFixture>
{
	private readonly RandomBreedImageByBreedCommand _model;
	private readonly Mock<IRandomBreedImageByBreedService> _randomBreedImageByBreedServiceMock;
	private readonly ListResponse<List<string>> _result;

	public RandomBreedImageByBreedTests(SharedFixture sharedFixture)
	{
		_model = sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
		_result = sharedFixture.ModelFakerFixture.ListResponseStringList.Generate();
		_randomBreedImageByBreedServiceMock = new Mock<IRandomBreedImageByBreedService>();
		_randomBreedImageByBreedServiceMock.Setup(x => x.RandomBreedImageByBreed(_model)).ReturnsAsync(_result);
	}

	[Fact]
	public async void RandomBreedImageByBreed_ShouldReturnListResponse()
	{
		var handler = new RandomBreedImageByBreedCommandHandler(_randomBreedImageByBreedServiceMock.Object);
		var result = await handler.Handle(_model, CancellationToken.None);

		result.Should().NotBeNull();
		result.List.Should().HaveCount(3);
		result.Should().BeOfType<ListResponse<List<string>>>();
	}
}

