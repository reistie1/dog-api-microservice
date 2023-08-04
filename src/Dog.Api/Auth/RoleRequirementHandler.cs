namespace Dog.Api.Auth;

public class RoleRequirementHandler : AuthorizationHandler<AdminRequirement>
{
	private readonly IOptions<JwtOptions> _jwtOptions;

	public RoleRequirementHandler(IOptions<JwtOptions> jwtOptions)
    {
		_jwtOptions = jwtOptions;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User != null)
        {
			var claims = context.User.Claims;
			var issuer = context.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Iss).Value;
			var roleClaims = claims.Where(x => x.Type == ClaimTypes.Role);

			if (roleClaims.Any(x => string.Equals(x.Value, "Admin", StringComparison.OrdinalIgnoreCase)) && string.Equals(issuer, _jwtOptions.Value.Issuer, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }
            else
            {
                var failure = new AuthorizationFailureReason(this, Constants.ErrorMessages.InvalidRoleRequirement);
                context.Fail(failure);
            }
        }

        return Task.CompletedTask;
    }
}
