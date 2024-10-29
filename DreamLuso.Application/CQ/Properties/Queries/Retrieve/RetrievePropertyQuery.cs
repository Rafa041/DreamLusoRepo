using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Properties.Queries.Retrieve;

public class RetrievePropertyQuery : IRequest<Result<RetrievePropertyResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}

