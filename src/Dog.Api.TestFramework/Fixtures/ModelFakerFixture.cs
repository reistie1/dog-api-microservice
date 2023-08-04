namespace Dog.Api.TestFramework.Fixtures;

public class ModelFakerFixture {
	public Faker<User> User { get; set; } = default!;
	public Faker<Role> Role { get; set; } = default!;
	public Faker<ListAllDogBreedsCommand> ListBreedsCommand { get; set; } = default!;
	public Faker<RandomBreedImageByBreedCommand> RandomBreedImageByBreed { get; set; } = default!;
	public Faker<JwtOptions> JwtOptions { get; set; } = default!;
	public Faker<SplitOptions> SplitOptions { get; set; } = default!;
	public Faker<Response<string>> ResponseString { get; set; } = default!;
	public Faker<ListResponse<List<string>>> ListResponseStringList { get; set; } = default!;

    public ModelFakerFixture() 
    {
        Role = new Faker<Role>()
            .ApplyRoleRules();

        User = new Faker<User>()
            .ApplyUserRules();

        ListBreedsCommand = new Faker<ListAllDogBreedsCommand>()
            .ApplyListAllBreedRules();

        RandomBreedImageByBreed = new Faker<RandomBreedImageByBreedCommand>()
            .ApplyRandomBreedImageByBreedRuleCollection();

		JwtOptions = new Faker<JwtOptions>()
			.ApplyJwtOptionsRuleCollection();

		SplitOptions = new Faker<SplitOptions>()
			.ApplySplitOptionsRuleCollection(new OptionsFixture());

		ResponseString = new Faker<Response<string>>()
			.ApplyResponseRuleCollection();

		ListResponseStringList = new Faker<ListResponse<List<string>>>()
			.ApplyListResponseBreedImagesRuleCollection();
    }
}
