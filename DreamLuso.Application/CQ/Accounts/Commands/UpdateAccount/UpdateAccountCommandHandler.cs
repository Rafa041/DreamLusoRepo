using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAccountPasswordCommand, Result<UpdateAccountPasswordResponse, Success, Error>>
{
    public async Task<Result<UpdateAccountPasswordResponse, Success, Error>> Handle(UpdateAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountRepository.GetByUserIdAsync(request.UserId);

        if (account == null)
            return Error.AccountNotFound;

        var isPasswordValid = unitOfWork.DataProtectionService.VerifyPassword(request.OldPassword, account.PasswordHash, account.PasswordSalt);

        if (!isPasswordValid)
            return Error.InvalidPassword;


        var protectionKeys = unitOfWork.DataProtectionService.Protect(request.NewPassword);

        account.PasswordHash = protectionKeys.PasswordHash;
        account.PasswordSalt = protectionKeys.PasswordSalt;

        await unitOfWork.AccountRepository.UpdateAsync(account, cancellationToken);

        await unitOfWork.CommitAsync();

        return new UpdateAccountPasswordResponse
        {
            UserId = request.UserId,
            Message = "Password updated successfully"
        };
    }
}
