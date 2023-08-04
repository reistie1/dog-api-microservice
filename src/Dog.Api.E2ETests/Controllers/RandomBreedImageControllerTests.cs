namespace Dog.Api.E2ETests.Controllers;

public class RandomBreedImageControllerTests:BaseTest, IClassFixture<SharedFixture>
{
	public RandomBreedImageControllerTests(SharedFixture sharedFixture) : base(sharedFixture.TestServerFixture)
	{
	}


	[Fact]
	public async Task RandomBreedImage_ShouldPass()
	{
		var result = await ApiClient.GetRandomBreedImage();

		result.Should().NotBeNull();
	}
}

