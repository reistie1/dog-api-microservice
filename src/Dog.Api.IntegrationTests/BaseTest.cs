namespace Dog.Api.IntegrationTests;

public abstract class BaseTest : IDisposable, IAsyncLifetime
{
	private readonly DogApiFixture _dogApiFixture = default!;

    protected BaseTest(DogApiFixture dogApiFixture) 
    {
        _dogApiFixture = dogApiFixture ?? throw new ArgumentNullException(nameof(dogApiFixture));
    }

	protected HttpClient Client { get; set; } = default!;

    public virtual Task InitializeAsync()
    {
        Client = _dogApiFixture.CreateClient();
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

        //Client?.Dispose();
        Client = null;
    }
}

