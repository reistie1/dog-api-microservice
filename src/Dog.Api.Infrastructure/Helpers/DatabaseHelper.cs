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
			if (_identityContext.Database.IsInMemory()) {
				_identityContext.Database.EnsureDeleted();
			}
			
            _identityContext.Database.EnsureCreated();
            _logger.LogInformation("Database successfully created");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new ErrorResult(ex.Message, ex);
        }
    }

    public async void SeedData()
    {
        try
        {
            var roleStore = new RoleStore<Role, IdentityContext, Guid>(_identityContext);
            var userStore = new UserStore<User, Role, IdentityContext, Guid>(_identityContext);

            var roles = new List<Role>
            {
                new Role { Id = Guid.Parse(Constants.AdminRole.Id), Name = Constants.AdminRole.Admin, NormalizedName = Constants.AdminRole.Admin.ToUpper() },
                new Role { Id = Guid.Parse(Constants.UserRole.Id), Name = Constants.UserRole.User, NormalizedName = Constants.UserRole.User.ToUpper() }
            };

            var users = new List<User>
            {
                new User { Id = Guid.Parse(Constants.User.Id), FirstName = "Bob", LastName = "Smith", Email = Constants.User.Email, UserName = Constants.User.Email, SecurityStamp = Guid.NewGuid().ToString(), NormalizedEmail = Constants.User.Email.ToUpper() },
                new User { Id = Guid.Parse(Constants.AdminUser.Id), FirstName = "Rob", LastName = "Perth", Email = Constants.AdminUser.Email, UserName = Constants.AdminUser.Email, SecurityStamp = Guid.NewGuid().ToString(), NormalizedEmail = Constants.AdminUser.Email.ToUpper() },
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
                    if (string.Equals(user.Email, Constants.AdminUser.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, Constants.AdminUser.Password);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, Constants.AdminRole.Admin.ToUpper());
                    }
                    else
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, Constants.User.Password);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, Constants.UserRole.User.ToUpper());
                    }
                }
            }

            _identityContext.SaveChanges();
			_logger.LogInformation("Database successfully seeded");
        }
        catch (Exception ex) 
        {
            _logger.LogError($"Error seeding database with identity user and roles, Error: {0}", ex.Message);
            throw new ErrorResult(ex.Message, ex);
        }
    }
}
