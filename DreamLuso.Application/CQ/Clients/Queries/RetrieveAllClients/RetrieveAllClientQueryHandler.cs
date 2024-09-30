using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Clients.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Queries.RetrieveAllClients;

public class RetrieveAllClientQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllClientQuery, Result<RetrieveAllClientResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllClientResponse, Success, Error>> Handle(RetrieveAllClientQuery request, CancellationToken cancellationToken)
    {
        var clients = await unitOfWork.ClientRepository.RetrieveAllAsync();

        if (clients == null || !clients.Any())
            return Error.EmptyDatabase;

        var clientResponses = clients.Select(client => new RetrieveClientResponse
        {
            Id = client.Id,
            UserId = client.UserId,
            IsActive = client.IsActive
        }).ToList();

        var response = new RetrieveAllClientResponse
        {
            Clients = clientResponses
        };
        return response;
    }
}