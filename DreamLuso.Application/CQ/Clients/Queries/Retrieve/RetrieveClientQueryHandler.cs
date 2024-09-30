using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Queries.Retrieve;

public class RetrieveClientQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveClientQuery, Result<RetrieveClientResponse, Success, Error>>
{
    public async Task<Result<RetrieveClientResponse, Success, Error>> Handle(RetrieveClientQuery request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.ClientRepository.RetrieveAsync(request.Id, cancellationToken);
        if (client == null)
            return Error.ClientNotFound;

        var response = new RetrieveClientResponse
        {
            Id = client.Id,
            UserId = client.UserId,
            IsActive = client.IsActive
        };
        return response;
    }
}