namespace Dog.Api.Application;

[Authorize(Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public partial class ListAllBreedsController : BaseController
{
    private readonly IMediator _mediator;

    public ListAllBreedsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Breeds>))]
    public async Task<ActionResult> ListAllBreeds([FromQuery] string? search, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(
            await _mediator.Send(new ListAllDogBreedsCommand
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search
            })
        );
    }
}

public class ListAllDogBreedsCommand : IRequest<List<Breeds>>
{
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string? Search { get; set; } = default!;
}

public class ListAllDogBreedsCommandValidator : AbstractValidator<ListAllDogBreedsCommand>
{
    public ListAllDogBreedsCommandValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage(Constants.ErrorMessages.PageNumberTooSmall)
            .NotEmpty().WithMessage(Constants.ErrorMessages.EmptyPageNumber);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage(Constants.ErrorMessages.PageSizeTooSmall)
            .LessThanOrEqualTo(20).WithMessage(Constants.ErrorMessages.PageSizeTooLarge)
            .NotEmpty().WithMessage(Constants.ErrorMessages.EmptyPageSize);

        RuleFor(x => x.Search)
            .Matches(new Regex(Constants.Validators.SearchRegex)).WithMessage(Constants.ErrorMessages.InvalidSearchFormat)
            .When(x => x.Search != null);
    }
}

public class ListAllDogBreedsCommandHandler : IRequestHandler<ListAllDogBreedsCommand, List<Breeds>>
{
    private readonly IListAllBreedsService _dogService;

    public ListAllDogBreedsCommandHandler(IListAllBreedsService dogService)
    {
        _dogService = dogService ?? throw new NullReferenceException(nameof(dogService));
    }

    public async Task<List<Breeds>> Handle(ListAllDogBreedsCommand request, CancellationToken cancellationToken)
    {
        return await _dogService.ListAllBreeds(request);
    }
}

public class ListAllBreedsService : IListAllBreedsService
{
    private readonly HttpClient _httpClient;

    public ListAllBreedsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<Breeds>> ListAllBreeds(ListAllDogBreedsCommand request)
    {
        List<Breeds> breedList = new();
        var response = await _httpClient.GetStringAsync("breeds/list/all");
        var result = JsonSerializer.Deserialize<Response<Dictionary<string, string[]>>>(response);

        if (result != null)
        {
            foreach (var breed in result.Message)
            {
                breedList.Add(new Breeds { Name = breed.Key, SubBreeds = breed.Value });
            }

            if (request.Search != null)
            {
                breedList = breedList
                    .Where(x => string.Equals(x.Name, request.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return breedList
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToList();
        }

        return breedList;
    }
}

public interface IListAllBreedsService
{
    Task<List<Breeds>> ListAllBreeds(ListAllDogBreedsCommand request);
}

public class Breeds
{
    public string Name { get; set; } = default!;
    public string[] SubBreeds { get; set; } = default!;
}
