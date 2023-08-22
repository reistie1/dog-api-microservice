namespace Dog.Api.Infrastructure.Helpers;

public class PasswordHasher : IPasswordHasher
{
	private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
	private readonly int _iterations = 350000;
	private readonly int _keySize = 64;

	public string HashPassword(User user, string password)
	{
		if (user != null) 
		{
			var salt = RandomNumberGenerator.GetBytes(_keySize);
			var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);
			user.PasswordSalt = Convert.ToBase64String(salt);
			return Convert.ToHexString(hash);
		}

		throw new ErrorResult(Constants.ErrorMessages.EmptyUser);
	}

	public bool VerifyPassword(User user, string password)
	{
		if (user != null && !string.IsNullOrEmpty(password)) 
		{
			var salt = Convert.FromBase64String(user.PasswordSalt);
			var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);

			if (user.PasswordHash != null) 
			{
				return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(user.PasswordHash));
			}
		}
		
		return false;
	}
}
