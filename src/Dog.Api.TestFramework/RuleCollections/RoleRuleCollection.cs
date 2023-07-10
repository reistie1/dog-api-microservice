using Bogus;
using Dog.Api.Core.Models;

namespace Dog.Api.TestFramework.RuleCollections;

public static class RoleRuleCollection
{
    public static Faker<TRole> ApplyRoleRules<TRole>(this Faker<TRole> faker) 
        where TRole: Role
    {
        return faker
            .RuleFor(x => x.Id, f => f.Random.Uuid())
            .RuleFor(x => x.Name, _ => "admin")
            .RuleFor(X => X.NormalizedName, _ => "Admin");
    }
}

