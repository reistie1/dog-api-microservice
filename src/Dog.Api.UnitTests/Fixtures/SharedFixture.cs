using Dog.Api.TestFramework.Fixtures;

namespace Dog.Api.UnitTests.Fixtures;
public class SharedFixture {

    private static readonly ModelFakerFixture _modelFakerFixture;
	private static readonly PasswordHasher _passwordHasher;
	private static readonly DatabaseFixture _databaseFixture;
	private static readonly OptionsFixture _optionsFixture;

    public ModelFakerFixture ModelFaker = _modelFakerFixture;
	public PasswordHasher PasswordHasher = _passwordHasher;
	public DatabaseFixture DatabaseFixture = _databaseFixture;
	public OptionsFixture OptionsFixture = _optionsFixture;


    static SharedFixture() 
    {
        _modelFakerFixture = new ModelFakerFixture();
		_passwordHasher = new PasswordHasher();
		_databaseFixture = new DatabaseFixture();
		_optionsFixture = new OptionsFixture();
    }
}

