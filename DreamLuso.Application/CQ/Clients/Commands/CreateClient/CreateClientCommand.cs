

using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Commands.CreateClient;

public class CreateClientCommand : IRequest<Result<CreateClientResponse, Success, Error>>
{
    public required Guid UserId { get; init; }

}
