namespace Dog.Api.TestFramework.Fixtures;

public class TestServerFixture : IDisposable
{
    private DogApiWebAppliationFactory _factory = default!;
    private bool _disposedValue;

    public HttpClient CreateApiClient()
    {
        if (_factory != null) return _factory.CreateClient();

        _factory = new DogApiWebAppliationFactory();
        return _factory.CreateClient();
    }

    public void Dispose ()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose ( bool disposing )
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            _factory.Dispose();
        }

        _disposedValue = true;
    }
}

