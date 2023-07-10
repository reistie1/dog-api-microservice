namespace Dog.Api.TestFramework;
public static class TestConstants 
{
    public static class DogApiClient 
    {
        public const string BaseAddress = "https://dog.ceo/api/";
    }

    public static class Split 
    {
        public const string ApiKey = "bvg87nelaujicfp9ntao68qhbmbrbmbljo39";
        public const string TreatmentName = "dog-api";
        public const string UserId = "7c2c9450-1b3f-11ee-a478-068dce69de28";
        public const int WaitTime = 10000;
    }

    public static class Jwt 
    {
        public const string Audience = "http://localhost:5000";
        public const string Issuer = "http://localhost:5000";
        public const string SigningKey = "sHG3x5uO2gkx6AkLT5AVSA==";
        public const int ExpiryTime = 60;
    }
}
