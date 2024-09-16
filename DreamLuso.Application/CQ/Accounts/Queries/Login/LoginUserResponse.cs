namespace DreamLuso.Application.CQ.Accounts.Queries.Login;

public class LoginUserResponse
{
    public required Guid Id { get; set; }
    public required string Token { get; set; }
}
