namespace Dog.Api.Core;

public static class Constants
{
    public static class AdminRole
    {
        public const string Id = "0fd17611-03b5-4e89-93e2-f3d1165d43d3";
        public const string Name = "Admin";
		public const string NormalizedName = "ADMIN";
    }

	public static class AdminUser
	{
		public const string Email = "kyle_admin@mailsac.com";
		public const string Id = "f8dc46e4-41c2-4bf1-8d34-d45335b0d2b9";
		public const string FirstName = "Rob";
		public const string LastName = "Perth";
		public const string NormalizedEmail = "KYLE_ADMIN@MAILSAC.COM";
		public const string Password = "Welcome1!";
		public const string SecurityStamp = "1e1d2e4e-8f2e-4be2-b081-937b9b5a889d";
	}

    public static class ErrorMessages
    {
		public const string EmptyEmail = "Email cannot be empty";
        public const string EmptyPageNumber = "Page number cannot be empty";
        public const string EmptyPageSize = "Page size cannot be empty";
		public const string EmptyPassword = "Password cannot be empty";
		public const string EmptyUser = "User cannot be null";
		public const string InvalidCredentials = "Error generating token, credentials invalid";
		public const string InvalidEmailFormat = "Invalid email format";
		public const string InvalidPasswordFormat = "Invalid password format, password must contain at least one lowercase, uppercase, symbol, number and be a6 characters long";
        public const string InvalidRoleRequirement = "Invalid role requirement, you must be an admin to access this resource";
        public const string InvalidSearchFormat = "Search value is invalid";
        public const string PageNumberTooSmall = "Page number should be greater than or equal to 1";
        public const string PageSizeTooLarge = "Page size should be less than or equal to 20";
        public const string PageSizeTooSmall = "Page size should be greater or equal to 1";
	}

	public static class FeatureFlag
	{
		public const string Active = "Feature Flag is active";
		public const string Inactive = "Feature Flag is inactive";
	}

	public static class SuccessMessages
	{
		public const string DatabaseCreated = "Database successfully created";
		public const string DatabaseSeeded = "Database successfully seeded";
	}

	public static class User
	{
		public const string Email = "bob_smith@mailsac.com";
		public const string FirstName = "Bob";
		public const string Id = "ec5abf68-c075-4a86-83b2-f5ac3d6e2534";
		public const string LastName = "Smith";
		public const string NormalizedEmail = "BOB_SMITH@MAILSAC.COM";
		public const string Password = "Welcome1!";
		public const string SecurityStamp = "2170a7c2-ed6B-4868-9bF4-98fda67eb3cd";
	}
	public static class UserRole
	{
		public const string Id = "1fc3ae92-ce44-4826-812d-c8741c11b7dc";
		public const string Name = "User";
		public const string NormalizedName = "USER";
	}

	public static class Validators
	{
		public const string EmailRegex = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
		public const string PasswordRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$";
		public const string SearchRegex = "^[a-zA-Z]+$";
	}
}
