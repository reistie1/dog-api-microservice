namespace Dog.Api.TestFramework.RuleCollections;

public static class ListAllBreedsRuleCollection 
{
    public static Faker<TCommand> ApplyListAllBreedRules<TCommand>(this Faker<TCommand> faker) where TCommand : ListAllDogBreedsCommand
    {
        return faker
            .RuleFor(x => x.PageNumber,f => f.Random.Number(1))
            .RuleFor(x => x.PageSize,f => f.Random.Number(10))
            .RuleFor(x => x.Search, _ => "hound");
    }
}
