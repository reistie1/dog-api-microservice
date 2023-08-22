namespace Dog.Api.Modules;

public class AutofacModule : Module
{
	private readonly IConfigurationRoot _config;
	private readonly IServiceCollection _services;

	public AutofacModule(IServiceCollection services, string environment, IConfigurationRoot config)
    {
		_config = config;
		_services = services;
	}

	protected override void Load(ContainerBuilder builder)
    {
		var identityOptions = new IdentityOptions();

        identityOptions.Password.RequireDigit = true;
        identityOptions.Password.RequireLowercase = true;
        identityOptions.Password.RequireNonAlphanumeric = true;
        identityOptions.Password.RequireUppercase = true;
        identityOptions.Password.RequiredLength = 6;
        identityOptions.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        identityOptions.Lockout.MaxFailedAccessAttempts = 5;
        identityOptions.Lockout.AllowedForNewUsers = true;

		// User settings.
        identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
		identityOptions.User.RequireUniqueEmail = true;

		// Configuration
		builder.RegisterInstance<IConfiguration>(_config);

		builder.RegisterType<UserManager<User>>()
            .As<UserManager<User>>()
            .SingleInstance();
        builder.RegisterType<RoleManager<Role>>()
            .As<RoleManager<Role>>()
            .SingleInstance();
        builder.RegisterType<SignInManager<User>>()
            .As<SignInManager<User>>()
            .SingleInstance();
        builder.Register(c => new UserStore<User, Role, IdentityContext, Guid>(c.Resolve<IdentityContext>()))
            .As<IUserStore<User>>()
            .SingleInstance();
        builder.Register(c => new RoleStore<Role, IdentityContext, Guid>(c.Resolve<IdentityContext>()))
            .As<IRoleStore<Role>>()
            .SingleInstance();
        builder.RegisterType<UserValidator<User>>()
            .As<IUserValidator<User>>()
            .SingleInstance();
        builder.RegisterType<PasswordValidator<User>>()
            .As<IPasswordValidator<User>>()
            .SingleInstance();
        builder.RegisterType<PasswordHasher<User>>()
            .As<IPasswordHasher<User>>()
            .SingleInstance();
        builder.RegisterType<UpperInvariantLookupNormalizer>()
            .As<ILookupNormalizer>()
            .SingleInstance();
        builder.RegisterType<RoleValidator<Role>>()
            .As<IRoleValidator<Role>>()
            .SingleInstance();
        builder.RegisterType<IdentityErrorDescriber>()
            .As<IdentityErrorDescriber>()
            .SingleInstance();
        builder.RegisterType<SecurityStampValidator<User>>()
            .As<ISecurityStampValidator>()
            .SingleInstance();
        builder.RegisterType<TwoFactorSecurityStampValidator<User>>()
            .As<ITwoFactorSecurityStampValidator>()
            .SingleInstance();
        builder.RegisterType<UserClaimsPrincipalFactory<User, Role>>()
            .As<IUserClaimsPrincipalFactory<User>>()
            .SingleInstance();
        builder.RegisterType<DefaultUserConfirmation<User>>()
            .As<IUserConfirmation<User>>()
            .SingleInstance();
        builder.RegisterType<HttpContextAccessor>()
            .As<IHttpContextAccessor>()
            .InstancePerLifetimeScope();
        builder.Register(x => new IdentityBuilder(typeof(User), typeof(Role), _services)
            .AddEntityFrameworkStores<IdentityContext>());
        builder.Register(x => identityOptions);

        // Authorization
        builder.RegisterType<AdminRequirement>()
            .As<IAuthorizationRequirement>().InstancePerLifetimeScope();
        builder.RegisterType<RoleRequirementHandler>()
            .As<IAuthorizationHandler>().InstancePerLifetimeScope();

        // HttpClient
        builder.Register(c => new HttpClient() { BaseAddress = new Uri("https://dog.ceo/api/") })
            .As<HttpClient>()
            .InstancePerLifetimeScope();

        // MediatR
        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(typeof(Program).Assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithRegistrationScope(RegistrationScope.Scoped)
            .Build());

        // MediatR pipeline middlewares
        builder.RegisterGeneric(typeof(ValidationBehaviour<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(ErrorBehaviour<,>))
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerLifetimeScope();

        // Interfaces
        builder.RegisterType<GenerateTokenController.IdentityService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<ListAllBreedsService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<RandomBreedImageByBreedService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<RandomBreedImageService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.RegisterType<FeatureFlagController.FeatureFlagService>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
		builder.RegisterType<PasswordHasher>()
			.AsImplementedInterfaces()
			.InstancePerLifetimeScope();

		// Logging
		builder.Register((c, p) => {
			var loggerFactory = new LoggerFactory();
			loggerFactory.AddSerilog();

			return loggerFactory;
		})
		.As<ILoggerFactory>()
		.InstancePerLifetimeScope();

		builder.RegisterGeneric(typeof(Logger<>))
			.As(typeof(ILogger<>))
			.InstancePerLifetimeScope();

        // Options Configuration
        builder.Register(c => Options.Create(_config.GetSection("JwtOptions").Get<JwtOptions>())).As<IOptions<JwtOptions>>().SingleInstance();
        builder.Register(c => Options.Create(_config.GetSection("SplitIO").Get<SplitOptions>())).As<IOptions<SplitOptions>>().SingleInstance();

		// Validators
		builder.RegisterType<ListAllDogBreedsCommandValidator>().As<IValidator<ListAllDogBreedsCommand>>().InstancePerDependency();
		builder.RegisterType<RandomBreedImageByBreedCommandValidator>().As<IValidator<RandomBreedImageByBreedCommand>>().InstancePerDependency().SingleInstance();
		builder.RegisterType<GenerateTokenController.TokenRequestCommandValidator>().As<IValidator<GenerateTokenController.TokenRequestCommand>>().InstancePerDependency().SingleInstance();

        // JsonSerializer Options
        new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			PropertyNameCaseInsensitive = true,
			NumberHandling = JsonNumberHandling.Strict,
        };

        base.Load(builder);
    }
}
