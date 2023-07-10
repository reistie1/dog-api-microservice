using Bogus;
using Dog.Api.Application;
using Dog.Api.Core.Models;
using Dog.Api.TestFramework.RuleCollections;

namespace Dog.Api.TestFramework.Fixtures;

public class ModelFakerFixture {
    public Faker<User> User { get; set; } = default!;
    public Faker<Role> Role { get; set; } = default!;
    public Faker<ListAllDogBreedsCommand> ListBreedsCommand { get; set; } = default!;
    public Faker<RandomBreedImageByBreedCommand> RandomBreedImageByBreed { get; set; } = default!;

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
    }
}
