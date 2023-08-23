namespace Dog.Api.Core.Errors;

public class ErrorResult : Exception
{
	public int ErrorCode { get; set; } = default!;
	public IDictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();

	public ErrorResult() { }

    public ErrorResult(string message) : base(message) { }

	public ErrorResult(string message, Exception exception) : base(message, exception) { }

	public ErrorResult(int errorCode, string message, Exception exception) : base(message, exception)
    {
        ErrorCode = errorCode;
    }

	public ErrorResult(int errorCode, string errorMessage) : base(errorMessage)
    {
        ErrorCode = errorCode;
    }

    public ErrorResult(int errorCode, string message, IDictionary<string, string> errorMessages) : base(message)
    {
        ErrorCode = errorCode;
        ErrorMessages = errorMessages;
    }
}