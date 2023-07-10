namespace Dog.Api.TestFramework;
public class DogApiWebAppliationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost ( IWebHostBuilder builder )
    {
        builder.ConfigureServices(options => {
            options.AddDbContext<IdentityContext>(options => {
                options.UseInMemoryDatabase("testDb");
            });
        });

        base.ConfigureWebHost(builder);
    }
}

