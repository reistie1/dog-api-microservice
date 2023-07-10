namespace Dog.Api.Dtos;

public class Response<T>
{
    public T message { get; set; } = default!;
    public string status { get; set; } = default!;
}
