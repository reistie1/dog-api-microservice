namespace Dog.Api.TestFramework.Fixtures;

public class OptionsFixture
{
	private readonly IConfigurationRoot _config;
	public IOptions<JwtOptions> jwtOptions { get; set; } = default!;
	public IOptions<SplitOptions> splitOptions { get; set; } = default!;

	public OptionsFixture()
	{
		_config = new ConfigurationBuilder()
			.AddJsonFile($"appsettings.Test.json", false)
			.AddUserSecrets(typeof(Program).GetTypeInfo().Assembly, true)
			.AddEnvironmentVariables()
			.SetBasePath(Directory.GetCurrentDirectory())
			.Build();
	}

	public void CreateJwtOptions()
	{
		var options = new JwtOptions 
		{
			Issuer = _config["JwtOptions:Issuer"] ?? string.Empty,
			Audience = _config["JwtOptions:Audience"] ?? string.Empty,
			SigningKey = _config["JwtOptions:SigningKey"] ?? string.Empty,
			ExpiryTime = Convert.ToInt32(_config["JwtOptions:ExpiryTime"])
		};
		jwtOptions = Options.Create(options);
	}

	public void CreateSplitOptions()
	{
		var options = new SplitOptions {
			ApiKey = _config["SplitIO:ApiKey"] ?? string.Empty,
			TreatmentName = _config["SplitIO:TreatmentName"] ?? string.Empty,
			UserId = _config["SplitIO:UserId"] ?? string.Empty,
			WaitTime = Convert.ToInt32(_config["SplitIO:WaitTime"]),
			};
		splitOptions = Options.Create(options);
	}
}

