namespace library.Domain.Domain.Infrastructure;

public class Result<T>
{
    public bool Success { get; }
    public string? ErrorMessage { get; }
    public T? Data { get; }

    private Result(bool success, string? errorMessage, T? data)
    {
        Success = success;
        ErrorMessage = errorMessage;
        Data = data;
    }

    public static Result<T> Ok(T data) => new(true, null, data);
    public static Result<T> Fail(string error) => new(false, error, default);
}
