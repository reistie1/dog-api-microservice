namespace Dog.Api.Core;

public static class Constants
{
    public static class User
    {
        public const string Id = "ec5abf68-c075-4a86-83b2-f5ac3d6e2534";
        public const string Email = "bob_smith@mailsac.com";
        public const string Password = "Welcome1!";
    }

    public static class AdminUser
    {
        public const string Id = "f8dc46e4-41c2-4bf1-8d34-d45335b0d2b9";
        public const string Email = "kyle_admin@mailsac.com";
        public const string Password = "Welcome1!";
    }

    public static class AdminRole
    {
        public const string Id = "0fd17611-03b5-4e89-93e2-f3d1165d43d3";
        public const string Admin = "Admin";
        
    }

    public static class UserRole
    {
        public const string Id = "1fc3ae92-ce44-4826-812d-c8741c11b7dc";
        public const string User = "User";
    }

    public static class ErrorMessages
    {
        public const string EmptyPageNumber = "Page number cannot be empty";
        public const string EmptyPageSize = "Page size cannot be empty";
        public const string InvalidCredentials = "Error generating token, credentials invalid";
        public const string InvalidRoleRequirement = "Invalid role requirement, you must be an admin to access this resource";
        public const string InvalidSearchFormat = "Search value is invalid";
        public const string PageNumberTooSmall = "Page number should be greater than or equal to 1";
        public const string PageSizeTooLarge = "Page size should be less than or equal to 20";
        public const string PageSizeTooSmall = "Page size should be greater or equal to 1";
    }

	public static class Validators
	{
		public const string SearchRegex = "^[a-zA-Z]+$";
	}
}
