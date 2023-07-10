namespace Dog.Api.Configuration
{
    public class ErrorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ErrorBehaviour<TRequest, TResponse>> _logger;

        public ErrorBehaviour(ILogger<ErrorBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

				if (ex is HttpRequestException)
				{
					var httpException = ex as HttpRequestException;
					throw new ErrorResult(httpException.Message, httpException.StatusCode.ToString());
				}
				else 
				{
					throw new ErrorResult($"Error encountered in pipeline, error: {0}", ex.Message);
				}
            }
        }
    }
}
