namespace Dog.Api.E2ETests.Controllers;

[CollectionDefinition("RandomBreedImageCollection", DisableParallelization = true)]
[Collection("RandomBreedImageCollection")]
public class RandomBreedImageControllerTests:BaseTest, IClassFixture<SharedFixture>
{
	public RandomBreedImageControllerTests(SharedFixture sharedFixture) : base(sharedFixture.TestServerFixture)
	{}

	[Fact]
	public async Task RandomBreedImage_ShouldPass()
	{
		var result = await ApiClient.GetRandomBreedImage();

		result.Should().NotBeNull();
	}
}

