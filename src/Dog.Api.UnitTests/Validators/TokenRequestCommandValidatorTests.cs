namespace Dog.Api.UnitTests.Validators;

public class TokenRequestCommandValidatorTests : IClassFixture<SharedFixture>
{
	private readonly SharedFixture _sharedFixture;
	private readonly TokenRequestCommandValidator _validator;

	public TokenRequestCommandValidatorTests(SharedFixture sharedFixture)
	{
		_sharedFixture = sharedFixture;
		_validator = new TokenRequestCommandValidator();
	}

	[Fact]
	public async void TokenRequestValidator_ShouldFailWithEmptyEmail()
	{
		var model = _sharedFixture.ModelFakerFixture.TokenRequestCommand.Generate();
		model.Email = string.Empty;

		var result = await _validator.ValidateAsync(model, CancellationToken.None);

		result.Should().NotBeNull();
		result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyEmail);
	}

	[Fact]
	public async void TokenRequestValidator_ShouldFailWithInvalidEmail()
	{
		var model = _sharedFixture.ModelFakerFixture.TokenRequestCommand.Generate();
		model.Email = TestConstants.BadUserData.Email;

		var result = await _validator.ValidateAsync(model, CancellationToken.None);

		result.Should().NotBeNull();
		result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.InvalidEmailFormat);
	}

	[Fact]
	public async void TokenRequestValidator_ShouldFailWithEmptyPassword()
	{
		var model = _sharedFixture.ModelFakerFixture.TokenRequestCommand.Generate();
		model.Password = string.Empty;

		var result = await _validator.ValidateAsync(model, CancellationToken.None);

		result.Should().NotBeNull();
		result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyPassword);
	}


	[Fact]
	public async void TokenRequestValidator_ShouldFailWithInvalidPassword()
	{
		var model = _sharedFixture.ModelFakerFixture.TokenRequestCommand.Generate();
		model.Password = TestConstants.BadUserData.Password;

		var result = await _validator.ValidateAsync(model, CancellationToken.None);

		result.Should().NotBeNull();
		result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.InvalidPasswordFormat);
	}
}
