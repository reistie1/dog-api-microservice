namespace Dog.Api.UnitTests.AuthHandler;

public class RoleRequirementHandlerTests 
{
	private readonly RoleRequirementHandler _handler = default!;
	private readonly AdminRequirement[] _requirements = default!;

    public RoleRequirementHandlerTests() 
    {
        _requirements = new[] { new AdminRequirement() };
		_handler = new RoleRequirementHandler();
    }

    [Fact]
    public async Task AdminRoleRequirement_ShouldFail() 
    {
		var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "User") });
		var authContext = new AuthorizationHandlerContext(_requirements, new ClaimsPrincipal(claimsIdentity), null);

		await _handler.HandleAsync(authContext);

        authContext.HasFailed.Should().BeTrue();
    }

    [Fact]
    public async Task AdminRoleRequirement_ShouldPass() 
	{
		var claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Admin") });
		var authContext = new AuthorizationHandlerContext(_requirements, new ClaimsPrincipal(claimsIdentity), null);

		await _handler.HandleAsync(authContext);

        authContext.HasSucceeded.Should().BeTrue();
    }
}
