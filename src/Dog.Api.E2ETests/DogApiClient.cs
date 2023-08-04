namespace Dog.Api.E2ETests;

public class DogApiClient
{
    private readonly HttpClient _client;

    public DogApiClient(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<string> GetFeatureFlagStatusAsync()
    {
        await GetAuthTokenAsync(Constants.User.Email, Constants.User.Password);
        var result = await _client.GetAsync("/FeatureFlag");

        return await result.Content.ReadAsStringAsync();
    }

    public async Task GetAuthTokenAsync(string email, string password)
    {
        var request = await _client.PostAsJsonAsync("/GenerateToken", new GenerateToken.TokenRequest { Email = email, Password = password });

        if (request.IsSuccessStatusCode)
        {
            var token = await request.Content.ReadAsStringAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<Breeds>> GetListAllBreedsAsync(int pageNumber, int pageSize, string search = null)
    {
        if (string.IsNullOrEmpty(search))
        {
            return await _client.GetFromJsonAsync<List<Breeds>>($"/ListAllBreeds?pageNumber={pageNumber}&pageSize={pageSize}");
        }
        else
        {
            return await _client.GetFromJsonAsync<List<Breeds>>($"/ListAllBreeds?pageNumber={pageNumber}&pageSize={pageSize}&search={search}");
        }
    }

    public async Task<Response<string>> GetRandomBreedImage()
    {
        await GetAuthTokenAsync(Constants.User.Email, Constants.User.Password);
        return await _client.GetFromJsonAsync<Response<string>>("/RandomBreedImage");
    }

    public async Task<ListResponse<List<string>>> GetRandomBreedImageByBreedAsync(int pageNumber, int pageSize, string breed)
    {
        await GetAuthTokenAsync(Constants.User.Email, Constants.User.Password);
        return await _client.GetFromJsonAsync<ListResponse<List<string>>>($"/RandomBreedImageByBreed?pageNumber={pageNumber}&pageSize={pageSize}&breed={breed}");
    }
}
