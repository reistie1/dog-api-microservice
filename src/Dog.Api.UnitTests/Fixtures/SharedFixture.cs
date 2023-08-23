namespace Dog.Api.UnitTests.Fixtures;

public class SharedFixture 
{
	private static readonly DatabaseFixture _databaseFixture;
	private static readonly ModelFakerFixture _modelFakerFixture;
	private static readonly OptionsFixture _optionsFixture;
	private static readonly PasswordHasher _passwordHasher;

	public DatabaseFixture DatabaseFixture = _databaseFixture;
	public ModelFakerFixture ModelFakerFixture = _modelFakerFixture;
	public OptionsFixture OptionsFixture = _optionsFixture;
	public PasswordHasher PasswordHasher = _passwordHasher;


    static SharedFixture() 
    {
		_databaseFixture = new DatabaseFixture();
		_modelFakerFixture = new ModelFakerFixture();
		_optionsFixture = new OptionsFixture();
		_passwordHasher = new PasswordHasher();
    }
}

