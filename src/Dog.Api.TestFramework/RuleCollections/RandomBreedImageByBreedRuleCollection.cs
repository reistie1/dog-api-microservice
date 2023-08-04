namespace Dog.Api.TestFramework.RuleCollections;

public static class RandomBreedImageByBreedRuleCollection 
{
    public static Faker<TCommand> ApplyRandomBreedImageByBreedRuleCollection<TCommand>(this Faker<TCommand> faker) where TCommand : RandomBreedImageByBreedCommand 
    {
        return faker
            .RuleFor(x => x.PageNumber,f => f.Random.Number(1))
            .RuleFor(x => x.PageSize,f => f.Random.Number(20))
            .RuleFor(x => x.Breed, _ => "hound");
    }
}

