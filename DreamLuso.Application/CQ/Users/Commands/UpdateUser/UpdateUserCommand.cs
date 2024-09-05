using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<UpdateUserResponse, Success, Error>>
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required Access Access { get; init; }
    public required string PhoneNumber { get; init; }
    public required string ImageUrl { get; init; }
    public required DateTime DateOfBirth { get; init; }
}
