namespace Dog.Api.UnitTests.Validators;

public class ListAllDogBreedsCommandValidators 
{
    private readonly ListAllDogBreedsCommandValidator _validator;
    private readonly SharedFixture _sharedFixture;

    public ListAllDogBreedsCommandValidators() 
    {
        _validator = new ListAllDogBreedsCommandValidator();
        _sharedFixture = new SharedFixture();
    }

    [Fact]
    public async void ListAllDogBreedsValidator_ShouldFailWithInvalidPageNumber() 
    {
        var model = _sharedFixture.ModelFaker.ListBreedsCommand.Generate();
        model.PageNumber = -1;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.PageNumberTooSmall);
    }

    [Fact]
    public async void ListAllDogBreedsValidator_ShouldFailWithEmptyPageNumber() 
    {
        var model = _sharedFixture.ModelFaker.ListBreedsCommand.Generate();
        model.PageNumber = default!;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyPageNumber);
    }

    [Fact]
    public async void ListAllDogBreedsValidator_ShouldFailWithInvalidPageSize()
    {
        var model = _sharedFixture.ModelFaker.ListBreedsCommand.Generate();
        model.PageSize = -1;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.PageSizeTooSmall);
    }

    [Fact]
    public async void ListAllDogBreedsValidator_ShouldFailWithEmptyPageSize() 
    {
        var model = _sharedFixture.ModelFaker.ListBreedsCommand.Generate();
        model.PageSize = default!;
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.EmptyPageSize);
    }

    [Fact]
    public async void ListAllDogBreedsValidator_ShouldFailWithInvalidSearch() 
    {
        var model = _sharedFixture.ModelFaker.ListBreedsCommand.Generate();
        model.Search = "bound1";
        var result = await _validator.ValidateAsync(model);

        result.Errors.Should().Contain(x => x.ErrorMessage == Constants.ErrorMessages.InvalidSearchFormat);
    }
}