namespace Dog.Api.Dtos
{
    public class ListResponse<T>
    {
        public T List { get; set; } = default!;
        public int Count { get; set; } = default!;
    }
}
