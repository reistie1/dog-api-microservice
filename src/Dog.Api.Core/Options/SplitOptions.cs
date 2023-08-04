namespace Dog.Api.Core.Options;

public class SplitOptions
{
    public string ApiKey { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string TreatmentName { get; set; } = default!;
	public int WaitTime { get; set; } = 10000;
}
