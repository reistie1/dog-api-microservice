namespace Dog.Api.TestFramework.Fixtures;

public class ModelFakerFixture 
{
	public Faker<JwtOptions> JwtOptions { get; set; } = default!;
	public Faker<ListAllDogBreedsCommand> ListBreedsCommand { get; set; } = default!;
	public Faker<ListResponse<List<string>>> ListResponseStringList { get; set; } = default!;
	public Faker<RandomBreedImageByBreedCommand> RandomBreedImageByBreed { get; set; } = default!;
	public Faker<Response<string>> ResponseString { get; set; } = default!;
	public Faker<Role> Role { get; set; } = default!;
	public Faker<SplitOptions> SplitOptions { get; set; } = default!;
	public Faker<GenerateTokenController.TokenRequestCommand> TokenRequestCommand { get; set; } = default!;
	public Faker<User> User { get; set; } = default!;
	
    public ModelFakerFixture() 
    {
		JwtOptions = new Faker<JwtOptions>()
			.ApplyJwtOptionsRuleCollection();

		ListBreedsCommand = new Faker<ListAllDogBreedsCommand>()
			.ApplyListAllBreedRules();

		ListResponseStringList = new Faker<ListResponse<List<string>>>()
			.ApplyListResponseBreedImagesRuleCollection();

		RandomBreedImageByBreed = new Faker<RandomBreedImageByBreedCommand>()
			.ApplyRandomBreedImageByBreedRuleCollection();

		ResponseString = new Faker<Response<string>>()
			.ApplyResponseRuleCollection();

		Role = new Faker<Role>()
			.ApplyRoleRules();

		SplitOptions = new Faker<SplitOptions>()
			.ApplySplitOptionsRuleCollection(new OptionsFixture());

		TokenRequestCommand = new Faker<GenerateTokenController.TokenRequestCommand>()
			.ApplyTokenRequestRuleCollection();

		User = new Faker<User>()
			.ApplyUserRules();
	}
}
