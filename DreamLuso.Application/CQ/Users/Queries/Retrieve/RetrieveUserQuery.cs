using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQuery : IRequest<Result<RetrieveUserResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
