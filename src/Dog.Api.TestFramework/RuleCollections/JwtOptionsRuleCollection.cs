namespace Dog.Api.TestFramework.RuleCollections;

public static class JwtOptionsRuleCollection
{
	public static Faker<TOptions> ApplyJwtOptionsRuleCollection<TOptions>(this Faker<TOptions> faker) where TOptions : JwtOptions
	{
		return faker
			.RuleFor(x => x.Issuer, _ => TestConstants.Jwt.Issuer)
			.RuleFor(x => x.Audience, _ => TestConstants.Jwt.Audience)
			.RuleFor(x => x.ExpiryTime, f => f.Random.Number(60, 120))
			.RuleFor(x => x.SigningKey, f => TestConstants.Jwt.SigningKey);
	}
}

