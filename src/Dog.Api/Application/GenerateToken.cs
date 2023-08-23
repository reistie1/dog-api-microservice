namespace Dog.Api.Application;

[ApiController]
[Route("[controller]")]
public class GenerateTokenController : BaseController
{
	private readonly IMediator _mediator;

	public GenerateTokenController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [AllowAnonymous]
    [HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<ActionResult> GenerateTokenAsync([FromBody] TokenRequestCommand tokenRequest)
    {
        return Ok(await _mediator.Send(tokenRequest));
    }

	public class TokenRequestCommandValidator : AbstractValidator<TokenRequestCommand>
	{
		public TokenRequestCommandValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage(Constants.ErrorMessages.EmptyEmail).WithSeverity(Severity.Warning)
				.Matches(Constants.Validators.EmailRegex).WithMessage(Constants.ErrorMessages.InvalidEmailFormat);


			RuleFor(x => x.Password)
				.NotEmpty().WithMessage(Constants.ErrorMessages.EmptyPassword).WithSeverity(Severity.Warning)
				.Matches(Constants.Validators.PasswordRegex).WithMessage(Constants.ErrorMessages.InvalidPasswordFormat);
		}
	}

	public class TokenRequestCommandHandler : IRequestHandler<TokenRequestCommand, string>
	{
		private readonly IIdentityService _identityService;

		public TokenRequestCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService ?? throw new NullReferenceException(nameof(IdentityError));
		}

		public async Task<string> Handle(TokenRequestCommand request, CancellationToken cancellationToken)
		{
			return await _identityService.GenerateAuthTokenAsync(request);
		}
	}

    public  class IdentityService : IIdentityService
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly ILogger<IdentityService> _logger;
		private readonly IPasswordHasher _passwordHasher;
		private readonly UserManager<User> _userManager;

		public IdentityService(IOptions<JwtOptions> jwtOptions, ILogger<IdentityService> logger, IPasswordHasher passwordHasher, UserManager<User> userManager)
        {
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<string> GenerateAuthTokenAsync(TokenRequestCommand tokenRequest)
        {
            var user = await _userManager.FindByEmailAsync(tokenRequest.Email);
			var passwordCheck = _passwordHasher.VerifyPassword(user, tokenRequest.Password);

			if (user != null && passwordCheck)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, tokenRequest.Email),
                };

                foreach (var role in userRoles)
                {
                    claims[claims.Length - 1] = new Claim(ClaimTypes.Role, role);
                }

                var token = new JwtSecurityToken(
                    _jwtOptions.Value.Issuer,
                    _jwtOptions.Value.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(_jwtOptions.Value.ExpiryTime),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            _logger.LogInformation("Invalid credentials for user with email: {0}", tokenRequest.Email);
			throw new ErrorResult(StatusCodes.Status400BadRequest, Constants.ErrorMessages.InvalidCredentials);
        }
    }

	public interface IIdentityService
	{
		public Task<string> GenerateAuthTokenAsync (TokenRequestCommand tokenRequest);
	}

	public class TokenRequestCommand:IRequest<string>
	{
		public string Email { get; set; } = default!;
		public string Password { get; set; } = default!;
	}
}
