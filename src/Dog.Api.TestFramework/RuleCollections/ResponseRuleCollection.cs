namespace Dog.Api.TestFramework.RuleCollections;

public static class ResponseRuleCollection
{
	public static Faker<TResponse> ApplyResponseRuleCollection<TResponse>(this Faker<TResponse> faker) where TResponse : Response<string>
	{
		return faker
			.RuleFor(x => x.Message, f => f.Random.Word());
	}
}
