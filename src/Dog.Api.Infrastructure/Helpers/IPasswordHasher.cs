namespace Dog.Api.Infrastructure.Helpers;

public interface IPasswordHasher
{
	string HashPassword (User user, string password);
	bool VerifyPassword (User user, string password);
}
