namespace Dog.Api.Auth;

public class RoleRequirementHandler : AuthorizationHandler<AdminRequirement>
{
    public RoleRequirementHandler()
    {
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (context.User != null)
        {
			var claims = context.User.Claims;
			var roleClaims = claims.Where(x => x.Type == ClaimTypes.Role);

            if (roleClaims.Any(x => string.Equals(x.Value, "Admin", StringComparison.OrdinalIgnoreCase)))
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
