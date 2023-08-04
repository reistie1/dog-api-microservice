namespace Dog.Api.E2ETests.Controllers;

public class RandomBreedImageByBreedControllerTests:BaseTest, IClassFixture<SharedFixture>
{
	public RandomBreedImageByBreedControllerTests(SharedFixture sharedFixture) : base(sharedFixture.TestServerFixture)
	{
	}

	[Fact]
	public async Task RandomBreedImageByBreed_ShouldPassWithBreed()
	{
		var result = await ApiClient.GetRandomBreedImageByBreedAsync(1, 10, "hound");

		result.Should().NotBeNull();
		result.List.Should().NotBeEmpty();
		result.List.Count.Should().Be(10);
		result.Count.Should().BeGreaterThan(0);
		result.Should().BeOfType<ListResponse<List<string>>>();
	}

	[Fact]
	public async Task RandomBreedImageByBreed_ShouldFailWithInvalidBreed()
	{
		await ApiClient.Invoking(x => x.GetRandomBreedImageByBreedAsync(1, 10, "cat")).Should().ThrowAsync<ErrorResult>();
	}
}

