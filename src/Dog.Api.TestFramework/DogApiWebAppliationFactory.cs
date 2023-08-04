namespace Dog.Api.TestFramework;
public class DogApiWebAppliationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
		builder.UseEnvironment("Test");

		builder.ConfigureTestServices(services => {
			
			if (services.Any(x => x.ServiceType == typeof(DbContextOptions))) 
			{
				services.RemoveAll(typeof(DbContextOptions));
			}

			services.AddDbContext<IdentityContext>(options => options.UseSqlite("DataSource=:memory:"));
		});

		base.ConfigureWebHost(builder);
    }
}
