namespace Dog.Api.Application;

[ApiController]
[Route("[controller]")]
public partial class GenerateToken : BaseController
{
    private readonly IIdentityService _identityService;

    public GenerateToken(IIdentityService identityService)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<ActionResult> GenerateTokenAsync([FromBody] TokenRequest tokenRequest)
    {
        return Ok(await _identityService.GenerateAuthToken(tokenRequest.Email, tokenRequest.Password));
    }

	public class TokenRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public partial interface IIdentityService
    {
        public Task<string> GenerateAuthToken(string email, string password);
    }

    public partial class IdentityService : IIdentityService
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

        public async Task<string> GenerateAuthToken(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
				var passwordCheck = _passwordHasher.VerifyPassword(user, password);

				if (user != null && passwordCheck)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SigningKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, email),
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

                _logger.LogInformation("Invalid credentials for user with email: {0}", email);
				throw new Exception(Constants.ErrorMessages.InvalidCredentials);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating authentication token, Message: {0}, Exception: {1}", ex.Message, ex.InnerException);
                throw new ErrorResult(ex.Message, ex);
            }
        }
    }
}

