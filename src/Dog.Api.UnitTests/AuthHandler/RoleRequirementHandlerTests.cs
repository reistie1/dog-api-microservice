namespace Dog.Api.UnitTests.AuthHandler;

public class RoleRequirementHandlerTests : IClassFixture<SharedFixture>
{
	private readonly RoleRequirementHandler _handler = default!;
	private readonly AdminRequirement[] _requirements = default!;

    public RoleRequirementHandlerTests(SharedFixture sharedFixture) 
    {
		sharedFixture.OptionsFixture.CreateJwtOptions();
		_handler = new RoleRequirementHandler(sharedFixture.OptionsFixture.jwtOptions);
		_requirements = new[] { new AdminRequirement() };
    }

	[Fact]
	public async Task AdminRoleRequirement_ShouldPass()
	{
		var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Admin"), new Claim("iss", TestConstants.Jwt.Issuer) });
		var authContext = new AuthorizationHandlerContext(_requirements, new ClaimsPrincipal(claimsIdentity), null);

		await _handler.HandleAsync(authContext);

		authContext.HasSucceeded.Should().BeTrue();
	}

	[Fact]
    public async Task AdminRoleRequirement_ShouldFail() 
    {
		var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User"), new Claim("iss", TestConstants.BadData.Issuer) });
		var authContext = new AuthorizationHandlerContext(_requirements, new ClaimsPrincipal(claimsIdentity), null);

		await _handler.HandleAsync(authContext);

        authContext.HasFailed.Should().BeTrue();
    }
}
