namespace Dog.Api.Core.Errors;

public class ErrorResult : Exception
{
    public string ErrorCode { get; set; }
    public IEnumerable<string> ErrorMessage { get; set; }

    public ErrorResult() { }

    public ErrorResult(string message) : base(message) { }

    public ErrorResult(string errorCode, string errorMessage) : base(errorMessage)
    {
        ErrorCode = errorCode;
    }

    public ErrorResult(string message, Exception exception) : base(message, exception) { }

    public ErrorResult(string errorCode, string message, Exception exception) : base(message, exception)
    {
        ErrorCode = errorCode;
    }

    public ErrorResult(string errorCode, string message, IEnumerable<string> errorMessages) : base(message)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessages;
    }
}