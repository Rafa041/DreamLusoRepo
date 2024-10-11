using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace DreamLuso.Application.CQ.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Result<CreateUserResponse, Success, Error>>
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string PhoneNumber { get; init; }
    public string? ImageUrl { get; set; }
    public IFormFile ImageFile { get; init; }
    public required DateTime DateOfBirth { get; set; }
}

