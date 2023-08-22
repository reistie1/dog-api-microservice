namespace Dog.Api.UnitTests.Modules;

public class AutofacModuleTests
{
	private readonly ILifetimeScope _scope;

	public AutofacModuleTests()
	{
		var serviceCollection = new ServiceCollection();
		var config = new ConfigurationBuilder()
			.AddJsonFile($"appsettings.Test.json", false)
			.SetBasePath(Directory.GetCurrentDirectory())
			.Build();

		var builder = new ContainerBuilder();
		var module = new AutofacModule(serviceCollection, "Test", config);
		builder.RegisterModule(module);

		var container = builder.Build();

		_scope = container.BeginLifetimeScope("httpRequest");
	}

	[Fact]
	public void RegisterOptions_ShouldPass()
	{
		var jwtOptions = _scope.Resolve<IOptions<JwtOptions>>();
		var splitOptions = _scope.Resolve<IOptions<SplitOptions>>();
		var logger = _scope.Resolve<ILoggerFactory>();

		jwtOptions.Should().NotBeNull();
		splitOptions.Should().NotBeNull();
		logger.Should().NotBeNull();
	}
}
