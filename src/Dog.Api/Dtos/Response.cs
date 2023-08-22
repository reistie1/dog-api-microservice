namespace Dog.Api.Dtos;

public class Response<T>
{

	[JsonPropertyName("message")]
    public T Message { get; set; } = default!;

	[JsonPropertyName("status")]
    public string Status { get; set; } = default!;
}
