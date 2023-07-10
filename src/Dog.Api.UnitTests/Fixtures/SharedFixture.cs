using Dog.Api.TestFramework.Fixtures;

namespace Dog.Api.UnitTests.Fixtures;
public class SharedFixture {

    private static readonly ModelFakerFixture _modelFakerFixture;

    public ModelFakerFixture ModelFaker = _modelFakerFixture;


    static SharedFixture() 
    {
        _modelFakerFixture = new ModelFakerFixture();
    }
}

