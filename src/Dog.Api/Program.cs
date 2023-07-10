var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .SetBasePath(Directory.GetCurrentDirectory())
    .Build();

var logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.WithProperty("Environment", configuration.GetValue<string>("Api:Environment"))
    .Enrich.WithProperty("ApplicationName", configuration ["Api:ApplicationName"])
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

Log.Logger = logger;

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dog Microservice", Version = "v1" });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using JWT scheme",
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement{
	{
		new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			}
		},
		new string[] {}
	}});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
        ValidIssuer = configuration.GetValue<string>("JwtOptions:Issuer"),
        ValidAudience = configuration.GetValue<string>("JwtOptions:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtOptions:SigningKey"))),
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.Requirements.Add(new AdminRequirement()));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(options => { options.RegisterModule(new AutofacModule(builder.Services, environment)); });
builder.Host.UseSerilog(logger);

var app = builder.Build();

// Create & Seed Database
var context = app.Services.GetRequiredService<IdentityContext>();
var helperLogger = app.Services.GetService<ILogger<DatabaseHelper>>();
var dbHelper = new DatabaseHelper(context, helperLogger);

dbHelper.CreateDatabase();
dbHelper.SeedData();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program {}
