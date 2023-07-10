namespace Dog.Api.Application;

[Authorize]
[ApiController]
[Route("[controller]")]
public partial class RandomBreedImageController : BaseController
{
    private readonly IMediator _mediator;

    public RandomBreedImageController(IMediator mediator) 
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    public async Task<ActionResult> RandomBreed()
    {
        return Ok(await _mediator.Send(new RandomBreedCommand()));
    }
}

public class RandomBreedCommand : IRequest<Response<string>> {}

public class RandomBreedCommandHandler : IRequestHandler<RandomBreedCommand, Response<string>>
{
    private readonly IRandomBreedImageService _breedService;

    public RandomBreedCommandHandler(IRandomBreedImageService breedService)
    {
        _breedService = breedService ?? throw new NullReferenceException(nameof(breedService));
    }

    public async Task<Response<string>> Handle(RandomBreedCommand request, CancellationToken cancellationToken)
    {
        return await _breedService.RandomBreedImage();
    }
}

public class RandomBreedImageService : IRandomBreedImageService
{
    private readonly HttpClient _httpClient;

    public RandomBreedImageService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Response<string>> RandomBreedImage()
    {
        var response = await _httpClient.GetStringAsync("breeds/image/random");
        var result = JsonSerializer.Deserialize<Response<string>>(response);

        if (result != null)
        {
            return result;
        }

        return result;
    }
}

public interface IRandomBreedImageService
{
    Task<Response<string>> RandomBreedImage();
}
