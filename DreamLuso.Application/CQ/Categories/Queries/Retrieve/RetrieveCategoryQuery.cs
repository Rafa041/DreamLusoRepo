using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using MediatR;


namespace DreamLuso.Application.CQ.Categories.Queries.Retrieve;

public class RetrieveCategoryQuery : IRequest<Result<RetrieveCategoryResponse, Success, Error>>
{
    public Guid Id { get; init; }
}

