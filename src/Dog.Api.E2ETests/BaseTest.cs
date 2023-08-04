namespace Dog.Api.E2ETests;

public abstract class BaseTest : IDisposable, IAsyncLifetime
{
    private readonly TestServerFixture _testServerfixture;

    protected BaseTest(TestServerFixture testServerFixture) 
    {
        _testServerfixture = testServerFixture ?? throw new ArgumentNullException(nameof(testServerFixture));
    }

    protected HttpClient Client { get; set; } = default!;
    protected DogApiClient ApiClient { get; set; } = default!;

    public virtual Task InitializeAsync()
    {
        Client = _testServerfixture.CreateApiClient();
        Client.BaseAddress = new Uri("http://localhost:5000");
        ApiClient = new DogApiClient(Client);

        return Task.CompletedTask;
    }

    public void Dispose ()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task DisposeAsync ()
    {
        return Task.CompletedTask;
    }

    protected virtual void Dispose ( bool disposing )
    {
        if (!disposing)
        {
            return;
        }

        Client?.Dispose();
        Client = null;
    }
}

