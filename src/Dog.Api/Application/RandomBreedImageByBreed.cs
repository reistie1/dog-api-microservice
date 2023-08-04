namespace Dog.Api.Application;

[Authorize]
[ApiController]
[Route("[controller]")]
public partial class RandomBreedImageByBreedController : BaseController
{
    private readonly IMediator _mediator;

    public RandomBreedImageByBreedController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponse<List<string>>))]
    public async Task<ActionResult> RandomBreed([FromQuery] string breed, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(
            await _mediator.Send(new RandomBreedImageByBreedCommand
            {
                Breed = breed,
                PageNumber = pageNumber,
                PageSize = pageSize
            })
        );
    }
}

public class RandomBreedImageByBreedCommand : IRequest<ListResponse<List<string>>> 
{
    public string Breed { get; set; } = default!;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class RandomBreedImageByBreedCommandValidator : AbstractValidator<RandomBreedImageByBreedCommand> {
    public RandomBreedImageByBreedCommandValidator() 
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage(Constants.ErrorMessages.PageNumberTooSmall)
            .NotEmpty().WithMessage(Constants.ErrorMessages.EmptyPageNumber);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage(Constants.ErrorMessages.PageSizeTooSmall)
            .LessThanOrEqualTo(20).WithMessage(Constants.ErrorMessages.PageSizeTooLarge)
            .NotEmpty().WithMessage(Constants.ErrorMessages.EmptyPageSize);

        RuleFor(x => x.Breed)
            .Matches(new Regex(Constants.Validators.SearchRegex)).WithMessage(Constants.ErrorMessages.InvalidSearchFormat)
            .When(x => x.Breed != null);
    }
}

public class RandomBreedImageByBreedCommandHandler : IRequestHandler<RandomBreedImageByBreedCommand, ListResponse<List<string>>>
{
    private readonly IRandomBreedImageByBreedService _breedService;

    public RandomBreedImageByBreedCommandHandler(IRandomBreedImageByBreedService breedService)
    {
        _breedService = breedService ?? throw new NullReferenceException(nameof(breedService));
    }

    public async Task<ListResponse<List<string>>> Handle(RandomBreedImageByBreedCommand request, CancellationToken cancellationToken)
    {
        return await _breedService.RandomBreedImageByBreed(request);
    }
}

public class RandomBreedImageByBreedService : IRandomBreedImageByBreedService
{
    private readonly HttpClient _httpClient;

    public RandomBreedImageByBreedService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ListResponse<List<string>>> RandomBreedImageByBreed(RandomBreedImageByBreedCommand request)
    {
        var listResponse = new ListResponse<List<string>>();
        var response = await _httpClient.GetStringAsync($"breed/{request.Breed}/images");
        var result = JsonSerializer.Deserialize<Response<string[]>>(response);

        if (result != null)
        {
            listResponse.List = result.Message
                .Where(x => x.Contains(request.Breed, StringComparison.OrdinalIgnoreCase))
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToList();
            listResponse.Count = result.Message.Count();

            return listResponse;
        }

        return listResponse;
    }
}

public interface IRandomBreedImageByBreedService
{
    Task<ListResponse<List<string>>> RandomBreedImageByBreed(RandomBreedImageByBreedCommand request);
}
