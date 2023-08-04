namespace Dog.Api.E2ETests.Controllers;

[CollectionDefinition("ListAllBreedsCollection", DisableParallelization = true)]
[Collection("ListAllBreedsCollection")]
public class ListAllBreedsControllerTests : BaseTest, IClassFixture<SharedFixture>
	{
	public ListAllBreedsControllerTests(SharedFixture sharedFixture) : base(sharedFixture.TestServerFixture)
	{
	}

	[Fact]
	public async Task ListAllDogBreeds_ShouldFailWhenNotInAdminRole()
	{
		await ApiClient.GetAuthTokenAsync(Constants.User.Email, Constants.User.Password);
		await ApiClient.Invoking(x => x.GetListAllBreedsAsync(1, 10)).Should().ThrowAsync<HttpRequestException>();
	}

	[Fact]
	public async Task ListAllDogBreeds_ShouldPassWithAdminRoleAndNoSearch()
	{
		await ApiClient.GetAuthTokenAsync(Constants.AdminUser.Email, Constants.AdminUser.Password);
		var result = await ApiClient.GetListAllBreedsAsync(1, 10);

		result.Should().NotBeNull();
		result.Should().HaveCount(10);
		result.Should().BeOfType<List<Breeds>>();
	}

	[Fact]
	public async Task ListAllDogBreeds_ShouldPassWithAdminRoleAndSearch()
	{
		await ApiClient.GetAuthTokenAsync(Constants.AdminUser.Email, Constants.AdminUser.Password);
		var result = await ApiClient.GetListAllBreedsAsync(1, 20, "hound");

		result.Should().NotBeNull();
		result.Should().HaveCount(1);
		result.Should().BeOfType<List<Breeds>>();
	}
}

