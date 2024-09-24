

using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Properties.Commands.CreateProperty;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Commands.CreateClient;

public class CreateClientCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateClientCommand, Result<CreateClientResponse, Success, Error>>
{
    public async Task<Result<CreateClientResponse, Success, Error>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.UserId);

        if (existingUser != null)
            return Error.UserNotFound;

        var newClient = new Client
        {
            UserId = request.UserId
        };

        await unitOfWork.ClientRepository.AddAsync(newClient, cancellationToken);

        await unitOfWork.CommitAsync();

        var response = new CreateClientResponse
        {
            UserId = request.UserId,
            UserClient = existingUser
        };

        return response;
    }
}
