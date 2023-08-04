namespace Dog.Api.IntegrationTests.Fixtures;

public class SharedFixture
{
    private static readonly DogApiFixture _dogApiFixture;
	private static readonly OptionsFixture _optionsFixture;
	private static readonly ModelFakerFixture _modelFakerFixture;

	public DogApiFixture DogApiFixture => _dogApiFixture;
	public OptionsFixture OptionsFixture => _optionsFixture;
	public ModelFakerFixture ModelFakerFixture => _modelFakerFixture;

	static SharedFixture ()
    {
        _dogApiFixture = new DogApiFixture();
		_optionsFixture = new OptionsFixture();
		_modelFakerFixture = new ModelFakerFixture();
	}
}

