using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Queries.RetrieveAllClients;

public class RetrieveAllClientQuery : IRequest<Result<RetrieveAllClientResponse, Success, Error>>
{
}
