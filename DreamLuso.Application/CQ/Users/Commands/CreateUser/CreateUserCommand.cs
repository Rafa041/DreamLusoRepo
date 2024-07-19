using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Result<CreateUserResponse, Success, Error>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ImageUrl { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
