namespace Dog.Api.UnitTests.Validators;

public class RandomBreedImageByBreedCommandValidators 
{
    private readonly RandomBreedImageByBreedCommandValidator _validator;
    private readonly SharedFixture _sharedFixture;

    public RandomBreedImageByBreedCommandValidators() 
    {
        _validator = new RandomBreedImageByBreedCommandValidator();
        _sharedFixture = new SharedFixture();
    }

    [Fact]
    public async void RandomBreedImageByBreedValidator_ShouldFailWithInvalidPageNumber() 
	{
        var model = _sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
        model.PageNumber = -1;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.PageNumberTooSmall);
    }

    [Fact]
    public async void RandomBreedImageByBreedValidator_ShouldFailWithEmptyPageNumber() 
	{
        var model = _sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
		model.PageNumber = default!;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyPageNumber);
    }

    [Fact]
    public async void RandomBreedImageByBreedValidator_ShouldFailWithInvalidPageSize() 
	{
        var model = _sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
        model.PageSize = -1;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.PageSizeTooSmall);
    }

    [Fact]
    public async void RandomBreedImageByBreedValidator_ShouldFailWithEmptyPageSize() 
	{
        var model = _sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
        model.PageSize = default!;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyPageSize);
    }

    [Fact]
    public async void RandomBreedImageByBreedValidator_ShouldFailWithInvalidBreed() 
	{
        var model = _sharedFixture.ModelFakerFixture.RandomBreedImageByBreed.Generate();
        model.Breed = "bound1";
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.InvalidSearchFormat);
    }
}
