namespace Dog.Api.IntegrationTests.Services;

public class RandomBreedImageServiceTests : BaseTest, IClassFixture<SharedFixture> {
    private readonly HttpClient _httpClient;

    public RandomBreedImageServiceTests(SharedFixture sharedFixture) : base(sharedFixture.DogApiFixture)
    {
        _httpClient = sharedFixture.DogApiFixture.CreateClient();
    }

    [Fact]
    public async Task RandomBreedImageService_ShouldPass() 
    {
        var service = new RandomBreedImageService(_httpClient);
        var result = await service.RandomBreedImage();

        result.Should().NotBeNull();
    }
}

