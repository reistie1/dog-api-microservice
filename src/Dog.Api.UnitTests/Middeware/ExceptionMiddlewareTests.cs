namespace Dog.Api.UnitTests.Middeware;

public class ExceptionMiddlewareTests
{
	private readonly DefaultHttpContext _httpContext;
	private readonly Mock<RequestDelegate> _mockRequestDelegate;
	private readonly Mock<ILogger<ExceptionMiddleware>> _mockLogger;

	public ExceptionMiddlewareTests()
	{
		_httpContext = new DefaultHttpContext();
		_mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
		_mockRequestDelegate = new Mock<RequestDelegate>();
		_mockRequestDelegate.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Returns(Task.FromResult(StatusCodes.Status200OK));
	}

	[Fact]
	public void ExceptionMiddleware_ShouldReturnOkStatus()
	{
		var exceptionMiddleware = new ExceptionMiddleware(_mockLogger.Object, _mockRequestDelegate.Object);

		_httpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
	}

	[Fact]
	public async void ExceptionMiddleware_ShouldReturn500Error()
	{
		_mockRequestDelegate.Setup(x => x.Invoke(_httpContext)).Throws(new Exception("Bad"));

		var exceptionMiddleware = new ExceptionMiddleware(_mockLogger.Object, _mockRequestDelegate.Object);
		await exceptionMiddleware.InvokeAsync(_httpContext);

		_httpContext.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
		_httpContext.Response.Body.Should().NotBeNull();
	}

	[Fact]
	public async void ExceptionMiddleware_ShouldReturn400Error()
	{
		_mockRequestDelegate.Setup(x => x.Invoke(_httpContext)).Throws(new ErrorResult(StatusCodes.Status400BadRequest, "Bad", new Dictionary<string, string>()));

		var exceptionMiddleware = new ExceptionMiddleware(_mockLogger.Object, _mockRequestDelegate.Object);
		await exceptionMiddleware.InvokeAsync(_httpContext);

		_httpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
		_httpContext.Response.Body.Should().NotBeNull();
	}

	[Fact]
	public async void ExceptionMiddleware_ShouldReturnHttpResponseExceptionStatusError()
	{
		_mockRequestDelegate.Setup(x => x.Invoke(_httpContext)).Throws(new HttpRequestException("Bad", new Exception(), HttpStatusCode.NotFound));

		var exceptionMiddleware = new ExceptionMiddleware(_mockLogger.Object, _mockRequestDelegate.Object);
		await exceptionMiddleware.InvokeAsync(_httpContext);

		_httpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
		_httpContext.Response.Body.Should().NotBeNull();
	}

	[Fact]
	public async void ExceptionMiddleware_ShouldReturn499StatusErrorWithErrorMessages()
	{
		var errorDictionary = new Dictionary<string, string> 
		{
			{ "Error", "Error1" }
		};
		_mockRequestDelegate.Setup(x => x.Invoke(_httpContext)).Throws(new ErrorResult(StatusCodes.Status400BadRequest, "Bad", errorDictionary));

		var exceptionMiddleware = new ExceptionMiddleware(_mockLogger.Object, _mockRequestDelegate.Object);
		await exceptionMiddleware.InvokeAsync(_httpContext);

		_httpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
		_httpContext.Response.Body.Should().NotBeNull();
	}
}

