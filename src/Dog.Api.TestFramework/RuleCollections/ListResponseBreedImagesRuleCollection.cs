namespace Dog.Api.TestFramework.RuleCollections;

public static class ListResponseBreedImagesRuleCollection
{
	public static Faker<TListResponse> ApplyListResponseBreedImagesRuleCollection<TListResponse>(this Faker<TListResponse> faker) where TListResponse : ListResponse<List<string>>
	{
		return faker
			.RuleFor(x => x.List, f => new List<string>() { f.Random.Word(), f.Random.Word(), f.Random.Word() })
			.RuleFor(x => x.Count, f => f.Random.Number());
	}
}
