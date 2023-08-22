namespace Dog.Api.Core.Options;

public class JwtOptions
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
	public int ExpiryTime { get; set; } = 60;
    public string SigningKey { get; set; } = default!;
}
