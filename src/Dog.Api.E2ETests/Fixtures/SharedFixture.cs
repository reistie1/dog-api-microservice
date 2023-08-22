namespace Dog.Api.IntegrationTests.Fixtures;

public class SharedFixture
{
    private static readonly ModelFakerFixture _modelFakerFixture;
    public static readonly TestServerFixture _testServerFixture;

    public TestServerFixture TestServerFixture => _testServerFixture;
    public ModelFakerFixture ModelFakerFixture => _modelFakerFixture;

    static SharedFixture()
    {
        _modelFakerFixture = new ModelFakerFixture();
        _testServerFixture = new TestServerFixture();
    }
}