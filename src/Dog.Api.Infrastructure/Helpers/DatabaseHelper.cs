namespace Dog.Api.Infrastructure.Helpers;

public class DatabaseHelper
{
    private readonly IdentityContext _identityContext;
    private readonly ILogger<DatabaseHelper> _logger;
	private readonly IPasswordHasher _passwordHasher;

	public DatabaseHelper(IdentityContext identityContext, ILogger<DatabaseHelper> logger, IPasswordHasher passwordHasher)
    {
        _identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public void CreateDatabase()
    {
        try
        {
			if (_identityContext.Database.IsInMemory()) 
			{
				_identityContext.Database.EnsureDeleted();
			}
			
            _identityContext.Database.EnsureCreated();
            _logger.LogInformation(Constants.SuccessMessages.DatabaseCreated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ErrorResult(ex.Message, ex);
        }
    }

    public async Task SeedData()
    {
        try
        {
            var roleStore = new RoleStore<Role, IdentityContext, Guid>(_identityContext);
            var userStore = new UserStore<User, Role, IdentityContext, Guid>(_identityContext);

            var roles = new List<Role>
            {
                new Role { Id = Guid.Parse(AdminRole.Id), Name = AdminRole.Name, NormalizedName = AdminRole.NormalizedName },
                new Role { Id = Guid.Parse(UserRole.Id), Name = UserRole.Name, NormalizedName = UserRole.NormalizedName }
            };

            var users = new List<User>
            {
                new User { Id = Guid.Parse(BasicUser.Id), FirstName = BasicUser.FirstName, LastName = BasicUser.LastName, Email = BasicUser.Email, UserName = BasicUser.Email, SecurityStamp = BasicUser.SecurityStamp, NormalizedUserName = BasicUser.NormalizedEmail, NormalizedEmail = BasicUser.NormalizedEmail },
                new User { Id = Guid.Parse(AdminUser.Id), FirstName = AdminUser.FirstName, LastName = AdminUser.LastName, Email = AdminUser.Email, UserName = AdminUser.Email, SecurityStamp = AdminUser.SecurityStamp, NormalizedUserName = AdminUser.NormalizedEmail, NormalizedEmail = AdminUser.NormalizedEmail },
            };

            if (!_identityContext.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await roleStore.CreateAsync(role);
                }
            }

            _identityContext.SaveChanges();

            if (!_identityContext.Users.Any())
            {
                foreach (var user in users)
                {
                    if (string.Equals(user.Email, AdminUser.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, AdminUser.Password);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, AdminRole.NormalizedName);
                    }
                    else
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, BasicUser.Password);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, UserRole.NormalizedName);
                    }
                }
            }

            _identityContext.SaveChanges();
			_logger.LogInformation(Constants.SuccessMessages.DatabaseSeeded);
        }
        catch (Exception ex) 
        {
            _logger.LogError($"Error seeding database with identity user and roles, Error: {0}", ex.Message);
            throw new ErrorResult(ex.Message, ex);
        }
    }
}
