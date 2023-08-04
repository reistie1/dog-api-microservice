namespace Dog.Api.Infrastructure.Contexts;

public class IdentityContext : IdentityDbContext<User, Role, Guid>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("AspNetUsers");
        builder.Entity<Role>().ToTable("AspNetRoles");
    }
}
