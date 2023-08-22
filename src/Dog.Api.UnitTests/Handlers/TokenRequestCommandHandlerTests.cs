namespace Dog.Api.UnitTests.Handlers;

public class TokenRequestCommandHandlerTests : IClassFixture<SharedFixture>
{
	private readonly TokenRequestCommand _command;
	private readonly Mock<IIdentityService> _identityServiceMock;

	public TokenRequestCommandHandlerTests(SharedFixture sharedFixture)
	{
		_command = sharedFixture.ModelFakerFixture.TokenRequestCommand.Generate();
		_identityServiceMock = new Mock<IIdentityService>();
		_identityServiceMock.Setup(x => x.GenerateAuthTokenAsync(_command)).ReturnsAsync("MyToken");
	}

	[Fact]
	public async void TokenRequestCommandHandler_ShouldReturnToken()
	{
		var handler = new TokenRequestCommandHandler(_identityServiceMock.Object);
		var result = await handler.Handle(_command, CancellationToken.None);

		result.Should().Be("MyToken");
	}

	[Fact]
	public async void TokenRequestCommandHandler_ShouldThrow()
	{
		_identityServiceMock.Setup(x => x.GenerateAuthTokenAsync(It.IsAny<TokenRequestCommand>())).ThrowsAsync(new ErrorResult());
		var handler = new TokenRequestCommandHandler(_identityServiceMock.Object);
		
		await handler.Invoking(x => x.Handle(new TokenRequestCommand(), CancellationToken.None)).Should().ThrowAsync<ErrorResult>();
	}
}