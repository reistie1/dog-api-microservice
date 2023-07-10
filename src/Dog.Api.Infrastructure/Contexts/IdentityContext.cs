namespace Dog.Api.Infrastructure.Contexts;

public class IdentityContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _config;


    public IdentityContext(DbContextOptions<IdentityContext> options, IConfiguration config) : base(options)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("AspNetUsers");
        builder.Entity<Role>().ToTable("AspNetRoles");
    }
}
