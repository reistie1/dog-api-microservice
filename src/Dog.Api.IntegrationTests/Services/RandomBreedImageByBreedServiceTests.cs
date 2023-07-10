namespace Dog.Api.IntegrationTests.Services;

public class RandomBreedImageByBreedServiceTests : BaseTest, IClassFixture<SharedFixture> 
{

    public RandomBreedImageByBreedServiceTests(SharedFixture sharedFixture) : base(sharedFixture.DogApiFixture)
    {
    }

    [Fact]
    public async Task RandomBreedImageByBreedService_ShouldPass() 
    {
        var model = new ModelFakerFixture().RandomBreedImageByBreed.Generate();
        var service = new RandomBreedImageByBreedService(Client);
        var result = await service.RandomBreedImageByBreed(model);

        result.Count.Should().BeGreaterThanOrEqualTo(0);
        result.List.Should().NotBeEmpty();
    }

	[Fact]
	public async Task RandomBreedImageByBreedService_ShouldFail()
	{
		var model = new ModelFakerFixture().RandomBreedImageByBreed.Generate();
		model.Breed = "Beagle";
		var service = new RandomBreedImageByBreedService(Client);

		await service.Invoking(x => x.RandomBreedImageByBreed(model)).Should().ThrowAsync<HttpRequestException>();
	}
}

