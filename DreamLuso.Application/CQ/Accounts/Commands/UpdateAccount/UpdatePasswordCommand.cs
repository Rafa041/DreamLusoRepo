

using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using MediatR;

namespace DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;

public class UpdateAccountPasswordCommand : IRequest<Result<UpdateAccountResponse, Success, Error>>
{
    public required Guid UserId { get; init; }
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }
}