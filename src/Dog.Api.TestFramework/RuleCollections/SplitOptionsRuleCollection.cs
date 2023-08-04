namespace Dog.Api.TestFramework.RuleCollections;

public static class SplitOptionsRuleCollection
{
	public static Faker<TOptions> ApplySplitOptionsRuleCollection<TOptions>(this Faker<TOptions> faker, OptionsFixture optionsFixture) where TOptions : SplitOptions
	{
		optionsFixture.CreateSplitOptions();

		return faker
			.RuleFor(x => x.WaitTime, _ => 10000)
			.RuleFor(x => x.UserId, _ => optionsFixture.splitOptions.Value.UserId)
			.RuleFor(x => x.ApiKey, f => optionsFixture.splitOptions.Value.ApiKey)
			.RuleFor(x => x.TreatmentName, f => f.Random.Word());
	}
}

