namespace Dog.Api.Dtos;

public class ErrorResponse
{
	public IDictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();
	public string Message { get; set; } = default!;
	public int StatusCode { get; set; } = default!;


	public ErrorResponse(int statusCode, IDictionary<string, string> errorMessages)
	{
		ErrorMessages = errorMessages;
		StatusCode = statusCode;
	}

	public ErrorResponse(int statusCode, IDictionary<string, string> errorMessages, string message)
	{
		ErrorMessages = errorMessages;
		Message = message;
		StatusCode = statusCode;
	}

	public ErrorResponse(int statusCode, string message)
	{
		Message = message;
		StatusCode = statusCode;
	}
}
