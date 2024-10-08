using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Accounts.Queries.Login;

public class LoginUserQuery : IRequest<Result<LoginUserResponse, Success, Error>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
