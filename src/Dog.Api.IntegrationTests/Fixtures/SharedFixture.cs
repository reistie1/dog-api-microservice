namespace Dog.Api.IntegrationTests.Fixtures;

public class SharedFixture
{
    private static readonly ModelFakerFixture _modelFakerFixture;
    private static readonly DogApiFixture _dogApiFixture;

    public ModelFakerFixture ModelFakerFixture => _modelFakerFixture;
    public DogApiFixture DogApiFixture => _dogApiFixture;

    static SharedFixture()
    {
        _modelFakerFixture = new ModelFakerFixture();
        _dogApiFixture = new DogApiFixture();
    }
}

