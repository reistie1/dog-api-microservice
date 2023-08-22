namespace Dog.Api.UnitTests.Handlers;

public class ListAllBreedsCommandHandlerTests : IClassFixture<SharedFixture>
{
	private readonly ListAllDogBreedsCommand _model;
	private readonly List<Breeds> _result;
	private readonly Mock<IListAllBreedsService> _listAllBreedsServiceMock;

	public ListAllBreedsCommandHandlerTests(SharedFixture sharedFixture)
	{
		_model = sharedFixture.ModelFakerFixture.ListBreedsCommand.Generate();
		_result = new List<Breeds>() { new Breeds() { Name = "beagle", SubBreeds = new string[] { "dog1", "dog2" }}};
		_listAllBreedsServiceMock = new Mock<IListAllBreedsService>();
		_listAllBreedsServiceMock.Setup(x => x.ListAllBreeds(_model)).ReturnsAsync(_result);
	}

	[Fact]
	public async void ListAllBreeds_ShouldReturnListOfBreeds()
	{
		var handler = new ListAllDogBreedsCommandHandler(_listAllBreedsServiceMock.Object);
		var result = await handler.Handle(_model, CancellationToken.None);

		result.Should().NotBeNull();
		result.Should().Equal(_result);
		result.Should().BeOfType<List<Breeds>>();
	}
}
