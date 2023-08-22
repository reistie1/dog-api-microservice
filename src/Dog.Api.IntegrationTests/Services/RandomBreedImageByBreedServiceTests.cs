namespace Dog.Api.IntegrationTests.Services;

public class RandomBreedImageByBreedServiceTests : BaseTest, IClassFixture<SharedFixture> 
{
	private readonly SharedFixture _sharedFixture;

	public RandomBreedImageByBreedServiceTests(SharedFixture sharedFixture) : base(sharedFixture.DogApiFixture)
    {
		_sharedFixture = sharedFixture;
    }

    [Fact]
    public async Task RandomBreedImageByBreedService_ShouldPass() 
    {
        var model = new ModelFakerFixture().RandomBreedImageByBreed.Generate();
        var service = new RandomBreedImageByBreedService(_sharedFixture.DogApiFixture.CreateClient());
        var result = await service.RandomBreedImageByBreed(model);

        result.Count.Should().BeGreaterThanOrEqualTo(0);
        result.List.Should().NotBeEmpty();
		result.Should().BeOfType<ListResponse<List<string>>>();
    }

	[Fact]
	public async Task RandomBreedImageByBreedService_ShouldFail()
	{
		var model = new ModelFakerFixture().RandomBreedImageByBreed.Generate();
		model.Breed = "Beagle";
		var service = new RandomBreedImageByBreedService(_sharedFixture.DogApiFixture.CreateClient());

		await service.Invoking(x => x.RandomBreedImageByBreed(model)).Should().ThrowAsync<HttpRequestException>();
	}
}
