using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;

public class UpdateAccountPasswordCommand : IRequest<Result<UpdateAccountPasswordResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}
