using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Clients.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<Result<UpdateClientResponse, Success, Error>>
{
    public Guid UserId { get; set; }
    public bool IsActive { get; init; }
}
