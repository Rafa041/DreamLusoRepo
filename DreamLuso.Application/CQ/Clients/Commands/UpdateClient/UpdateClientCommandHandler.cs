using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateClientCommand, Result<UpdateClientResponse, Success, Error>>
{
    public async Task<Result<UpdateClientResponse, Success, Error>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.UserId);

        if (existingUser != null)
            return Error.UserNotFound;

        var newClient = new Client
        {
            IsActive = request.IsActive
        };

        await unitOfWork.ClientRepository.UpdateAsync(newClient, cancellationToken);

        await unitOfWork.CommitAsync();

        var response = new UpdateClientResponse
        {
            UserId = request.UserId,
            UserClient = existingUser,
            IsActive = request.IsActive
        };

        return response;
    }
}
