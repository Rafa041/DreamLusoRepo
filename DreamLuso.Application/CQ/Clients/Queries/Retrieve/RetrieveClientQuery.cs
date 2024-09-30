using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using MediatR;

namespace DreamLuso.Application.CQ.Clients.Queries.Retrieve;

public class RetrieveClientQuery : IRequest<Result<RetrieveClientResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
