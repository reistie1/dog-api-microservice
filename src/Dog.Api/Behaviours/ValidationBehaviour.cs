namespace Dog.Api.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator) 
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validator.Select(x => x.ValidateAsync(context)));
            var failures = validationResults.SelectMany(x => x.Errors).Where(f =>  f != null).ToList();

            if (failures.Count > 0)
            {
                throw new ErrorResult(StatusCodes.Status400BadRequest.ToString(), "Validation Error", failures.Select(x => x.ErrorMessage).ToList());
            }
        }

        return await next();
    }
}
