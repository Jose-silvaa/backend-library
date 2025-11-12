namespace library.Domain.Domain.User.Read.Dto;

public class LoginResult
{
    public bool IsSuccess { get; private set; }
    public string? Token { get; private set; }
    public string? Error { get; private set; }

    public static LoginResult Success(string token)
        => new() { IsSuccess = true, Token = token };

    public static LoginResult Fail(string error)
        => new() { IsSuccess = false, Error = error };
}
