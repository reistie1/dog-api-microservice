namespace Dog.Api.TestFramework;

public static class TestConstants
{
	public static class BadUserData
	{
		public const string Email = "test+_))1@test.com";
		public const string Password = "password1";
	}

	public static class BadData
	{
		public const string Issuer = "http://localhost:5001";
	}

	public static class DogApiClient 
    {
        public const string BaseAddress = "https://dog.ceo/api/";
    }

    public static class Jwt 
    {
        public const string Audience = "http://localhost:5000";
        public const string Issuer = "http://localhost:5000";
        public const string SigningKey = "sHG3x5uO2gkx6AkLT5AVSA==";
        public const int ExpiryTime = 60;
    }
}
