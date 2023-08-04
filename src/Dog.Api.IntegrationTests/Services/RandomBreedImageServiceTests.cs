namespace Dog.Api.IntegrationTests.Services;

public class RandomBreedImageServiceTests : BaseTest, IClassFixture<SharedFixture> 
{
	private readonly SharedFixture _sharedFixture;

	public RandomBreedImageServiceTests(SharedFixture sharedFixture) : base(sharedFixture.DogApiFixture)
    {
		_sharedFixture = sharedFixture;
    }

    [Fact]
    public async Task RandomBreedImageService_ShouldPass() 
    {
        var service = new RandomBreedImageService(_sharedFixture.DogApiFixture.CreateClient());
        var result = await service.RandomBreedImage();

        result.Should().NotBeNull();
    }
}

