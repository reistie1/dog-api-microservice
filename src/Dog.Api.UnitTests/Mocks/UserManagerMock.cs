namespace Dog.Api.UnitTests.Mocks;

public static class UserManagerMock
{
    public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : User 
    {

        return new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);
    }
}
