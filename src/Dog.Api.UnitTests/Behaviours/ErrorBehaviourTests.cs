namespace Dog.Api.UnitTests.Behaviours;

public class ErrorBehaviourTests
{
	private readonly Mock<RequestHandlerDelegate<Response<string>>> _requestDelegateMock;
	private readonly ILogger<ErrorBehaviour<RandomBreedCommand, Response<string>>> _logger;

	public ErrorBehaviourTests()
	{
		_requestDelegateMock = new Mock<RequestHandlerDelegate<Response<string>>>();
		_requestDelegateMock.Setup(x => x()).ThrowsAsync(new Exception("BadRequest"));
		_logger = new LoggerFactory().CreateLogger<ErrorBehaviour<RandomBreedCommand, Response<string>>>();
	}

	[Fact]
	public async void ErrorBehaviourTest_ShouldThrowErrorResultForPipelineError()
	{
		var errorBehaviour = new ErrorBehaviour<RandomBreedCommand, Response<string>>(_logger);

		await errorBehaviour.Invoking(x => x.Handle(new RandomBreedCommand(), _requestDelegateMock.Object, CancellationToken.None)).Should()
			.ThrowAsync<ErrorResult>()
			.WithMessage("BadRequest");
	}

	[Fact]
	public async void ErrorBehaviourTest_ShouldThrowErrorResultForHttpRequest()
	{
		_requestDelegateMock.Setup(x => x()).ThrowsAsync(new HttpRequestException("Http Error", new Exception("Can't access"), HttpStatusCode.Forbidden));
		var errorBehaviour = new ErrorBehaviour<RandomBreedCommand, Response<string>>(_logger);

		await errorBehaviour.Invoking(x => x.Handle(new RandomBreedCommand(), _requestDelegateMock.Object, CancellationToken.None)).Should()
			.ThrowAsync<ErrorResult>()
			.WithMessage("Forbidden");
	}

	[Fact]
	public async void ErrorBehaviourTest_ShouldNotReturnError()
	{
		_requestDelegateMock.Setup(x => x()).ReturnsAsync(new Response<string>() { Message = "Beagle" });
		var errorBehaviour = new ErrorBehaviour<RandomBreedCommand, Response<string>>(_logger);

		var result = await errorBehaviour.Handle(new RandomBreedCommand(), _requestDelegateMock.Object, CancellationToken.None);

		result.Should().NotBeNull();
	}
}

