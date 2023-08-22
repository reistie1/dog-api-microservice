namespace Dog.Api.TestFramework.RuleCollections;

public static class TokenRequestRuleCollection
{
	public static Faker<TOptions> ApplyTokenRequestRuleCollection<TOptions>(this Faker<TOptions> faker) where TOptions : GenerateTokenController.TokenRequestCommand
	{
		return faker
			.RuleFor(x => x.Email, Constants.AdminUser.Email)
			.RuleFor(x => x.Password, Constants.AdminUser.Password);
	}
}

