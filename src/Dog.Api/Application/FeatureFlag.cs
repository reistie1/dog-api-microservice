namespace Dog.Api.Application;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FeatureFlagController : BaseController
{
    private readonly IMediator _mediator;

    public FeatureFlagController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(string))]
    public async Task<ActionResult> GetFeatureFlag()
    {
        var result = await _mediator.Send(new FeatureFlagCommand());

        if (result)
        {
            return Ok("Feature Flag is active");
        }

        return Ok("Feature Flag is inactive");
    }

    public class FeatureFlagCommand : IRequest<bool>
    { }

    public class FeatureFlagCommandHandler : IRequestHandler<FeatureFlagCommand, bool>
    {
        private readonly IFeatureFlagService _featureFlagService;

        public FeatureFlagCommandHandler(IFeatureFlagService featureFlagService) 
        {
            _featureFlagService = featureFlagService ?? throw new ArgumentNullException(nameof(featureFlagService));
        }

        public Task<bool> Handle(FeatureFlagCommand request, CancellationToken cancellationToken)
        {
            return _featureFlagService.CheckFeatureFlag();
        }
    }

    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly IOptions<SplitOptions> _splitOptions;

        public FeatureFlagService(IOptions<SplitOptions> splitOptions)
        {
            _splitOptions = splitOptions ?? throw new ArgumentNullException(nameof(splitOptions));
        }

        public Task<bool> CheckFeatureFlag()
        {
            var config = new ConfigurationOptions();
            config.ReadTimeout = _splitOptions.Value.WaitTime;

            var factory = new SplitFactory(_splitOptions.Value.ApiKey, config);

            var splitClient = factory.Client();
            splitClient.BlockUntilReady(_splitOptions.Value.WaitTime);
            var treatment = splitClient.GetTreatment(_splitOptions.Value.UserId, _splitOptions.Value.TreatmentName);

            if (treatment == "on")
            {
                return Task.FromResult(true);
            }
            
            splitClient.Destroy();
            return Task.FromResult(false);
        }
    }

    public interface IFeatureFlagService
    {
        Task<bool> CheckFeatureFlag();
    } 
}
