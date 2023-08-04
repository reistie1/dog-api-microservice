namespace Dog.Api.Core.Models;

public class User : IdentityUser<Guid>
{
    public User() { }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
	public string PasswordSalt { get; set; } = default!;
}
