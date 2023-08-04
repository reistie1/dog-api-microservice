namespace Dog.Api.IntegrationTests.Services;

public class ListAllBreedsServiceTests : BaseTest, IClassFixture<SharedFixture>
{
    private readonly SharedFixture _sharedFixture;

    public ListAllBreedsServiceTests(SharedFixture sharedFixture) : base(sharedFixture.DogApiFixture)
    {
        _sharedFixture = sharedFixture;
    }

    [Fact]
    public async Task ListAllBreedsService_ShouldPass() 
    {
        var model = new ModelFakerFixture().ListBreedsCommand.Generate();
        var service = new ListAllBreedsService(_sharedFixture.DogApiFixture.CreateClient());
        var result = await service.ListAllBreeds(model);

        result.Should().NotBeEmpty();
        result.First().SubBreeds.Should().HaveCount(7);
        result.First().Name.Should().Be("hound");
    }

    [Fact]
    public async Task ListAllBreedsService_ShouldFail()
    {
        var model = new ModelFakerFixture().ListBreedsCommand.Generate();
        model.Search = "wolf";
        var service = new ListAllBreedsService(_sharedFixture.DogApiFixture.CreateClient());
        var result = await service.ListAllBreeds(model);

        result.Should().BeEmpty();
    }
}
