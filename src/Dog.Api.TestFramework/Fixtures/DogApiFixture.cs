namespace Dog.Api.TestFramework.Fixtures;

public class DogApiFixture
{
	private bool _disposedValue;
	private HttpClient _factory = default!;

    public HttpClient CreateClient() 
    {
        if(_factory != null) 
        {
            return _factory;
        }

        _factory = new HttpClient() { BaseAddress = new Uri(TestConstants.DogApiClient.BaseAddress) };  
        return _factory;
    }

    public void Dispose() 
	{
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) 
	{
        if(_disposedValue) {
            return;
        }

        if(disposing) {
            _factory.Dispose();
        }

        _disposedValue = true;
    }
}

