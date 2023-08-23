namespace Dog.Api.Middleware;

public class ExceptionMiddleware
{
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
	{
		_logger = logger;
		_next = next;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try 
		{
			await _next(httpContext);
		}
		catch(Exception ex) 
		{
			_logger.LogError(ex.Message, ex);

			if(ex is HttpRequestException) 
			{
				var exception = ex as HttpRequestException;
				await HandleExceptionAsync(httpContext, new ErrorResult((int)exception.StatusCode, exception.Message));
			}
			else if (ex is ErrorResult) 
			{
				var exception = ex as ErrorResult;
				await HandleExceptionAsync(httpContext, exception);
			}
			else 
			{
				await HandleExceptionAsync(httpContext, new ErrorResult(StatusCodes.Status500InternalServerError, ex.Message));
			}
		}
	}

	private async Task HandleExceptionAsync(HttpContext context, ErrorResult exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = exception.ErrorCode;

		if (exception.ErrorMessages.Any()) 
		{
			await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(context.Response.StatusCode, exception.ErrorMessages, exception.Message)));
		}
		else 
		{
			await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(context.Response.StatusCode, exception.Message)));
		}
	}
}