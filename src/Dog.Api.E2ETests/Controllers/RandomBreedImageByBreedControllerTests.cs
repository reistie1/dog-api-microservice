using System.Web.Http;

namespace Dog.Api.IntegrationTests.Controllers;

public class RandomBreedImageByBreedControllerTests : BaseTest, IClassFixture<SharedFixture>
{
    public RandomBreedImageByBreedControllerTests ( SharedFixture sharedFixture ) : base(sharedFixture.TestServerFixture)
    {}

    [Fact]
    public async Task RandomBreedImageByBreed_ShouldPassWithBreed()
    {
        var result = await ApiClient.GetRandomBreedImageByBreedAsync(1, 10, "hound");

        result.Should().NotBeNull();
        result.List.Should().NotBeEmpty();
        result.List.Count.Should().Be(10);
        result.Count.Should().BeGreaterThan(0);
    }
}

