using Bogus;
using Dog.Api.Core.Models;

namespace Dog.Api.TestFramework.RuleCollections;

public static class UserRuleCollection
{
    public static Faker<TUser> ApplyUserRules<TUser>(this Faker<TUser> faker) where TUser : User
    {
        return faker
            .RuleFor(x => x.Id, f => f.Random.Uuid())
            .RuleFor(x => x.SecurityStamp, f => f.Random.Uuid().ToString())
            .RuleFor(x => x.Email, _ => "dog_admin@mailsac.com")
            .RuleFor(x => x.NormalizedEmail, _ => "dog_admin@mailsac.com".ToUpper())
            .RuleFor(x => x.PhoneNumber, f => f.Person.Phone)
            .RuleFor(x => x.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.LastName, f => f.Person.LastName)
            .RuleFor(x => x.PasswordHash, f => f.Phone.Random.Hash())
            .RuleFor(x => x.UserName, f => f.Person.UserName);
    }
}

