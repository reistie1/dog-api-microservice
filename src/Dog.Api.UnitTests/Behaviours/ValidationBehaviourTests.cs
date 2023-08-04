namespace Dog.Api.UnitTests.Behaviours;

public class ValidationBehaviourTests :IClassFixture<SharedFixture>
{
	private readonly Mock<RequestHandlerDelegate<List<Breeds>>> _requestDelegateMock;
	private readonly SharedFixture _sharedFixture;

	public ValidationBehaviourTests(SharedFixture sharedFixture)
	{
		_requestDelegateMock = new Mock<RequestHandlerDelegate<List<Breeds>>>();
		_sharedFixture = sharedFixture;
	}

	[Fact]
	public async void ValidationBehaviourTest_ShouldReturnErro()
	{
		var model = _sharedFixture.ModelFakerFixture.ListBreedsCommand.Generate();
		model.PageNumber = -1;
		var context = new ValidationContext<ListAllDogBreedsCommand>(model);
		var validatorList = new List<IValidator<ListAllDogBreedsCommand>>() { new ListAllDogBreedsCommandValidator() };

		var validationBehaviour = new ValidationBehaviour<ListAllDogBreedsCommand, List<Breeds>>(validatorList);
		await validationBehaviour.Invoking(x => x.Handle(model, _requestDelegateMock.Object, CancellationToken.None)).Should().ThrowAsync<ErrorResult>();
	}

	[Fact]
	public async void ValidationBehaviourTest_ShoudntReturnError()
	{
		var model = _sharedFixture.ModelFakerFixture.ListBreedsCommand.Generate();
		model.PageNumber = -1;
		var context = new ValidationContext<ListAllDogBreedsCommand>(model);
		var validatorList = new List<IValidator<ListAllDogBreedsCommand>>() { };

		_requestDelegateMock.Setup(x => x()).ReturnsAsync(new List<Breeds>());
		var validationBehaviour = new ValidationBehaviour<ListAllDogBreedsCommand, List<Breeds>>(validatorList);
		var result = await validationBehaviour.Handle(model, _requestDelegateMock.Object, CancellationToken.None);

		result.Should().NotBeNull();
	}
}

