namespace Dog.Api.Modules;

public class AutofacModule : Module
{
    private readonly IServiceCollection _services;
    private readonly string _environment;

    public AutofacModule(IServiceCollection services, string environment)
    {
        _services = services;
        _environment = environment;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{_environment}.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        var identityOptions = new IdentityOptions();

        identityOptions.Password.RequireDigit = true;
        identityOptions.Password.RequireLowercase = true;
        identityOptions.Password.RequireNonAlphanumeric = true;
        identityOptions.Password.RequireUppercase = true;
        identityOptions.Password.RequiredLength = 6;
        identityOptions.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        identityOptions.Lockout.MaxFailedAccessAttempts = 5;
        identityOptions.Lockout.AllowedForNewUsers = true;

        // User settings.
        identityOptions.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        identityOptions.User.RequireUniqueEmail = false;

        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString(configuration.GetConnectionString("DefaultConnection")));

        builder.RegisterControllers(Assembly.GetExecutingAssembly());

        // Configuration
        builder.RegisterInstance<IConfiguration>(configuration);

        // IdentityContext
        builder.Register(c =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString(configuration.GetConnectionString("DefaultConnection")));
            return optionsBuilder.Options;
        })
            .As<DbContextOptions>()
            .As<DbContextOptions<IdentityContext>>()
            .SingleInstance();

        // Identity Configuration
        builder.Register(x => new IdentityContext(optionsBuilder.Options, configuration))
            .As<IdentityContext>()
            .SingleInstance();
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
        builder.RegisterType<GenerateToken.IdentityService>()
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

        // Logging
        builder.Register(( c, p ) =>
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();

            return loggerFactory;
        })
        .As<ILoggerFactory>()
        .SingleInstance();

        builder.RegisterGeneric(typeof(Logger<>))
            .As(typeof(ILogger<>))
            .SingleInstance();

        // Options Configuration
        builder.Register(c => Options.Create(configuration.GetSection("JwtOptions").Get<JwtOptions>())).As<IOptions<JwtOptions>>().SingleInstance();
        builder.Register(c => Options.Create(configuration.GetSection("SplitIO").Get<SplitOptions>())).As<IOptions<SplitOptions>>().SingleInstance();


        // JsonSerializer Options
        new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.Strict,
        };

        base.Load(builder);
    }
}
