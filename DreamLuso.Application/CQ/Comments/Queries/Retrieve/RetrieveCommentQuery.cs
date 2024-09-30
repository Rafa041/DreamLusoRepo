using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Comments.Queries.Retrieve;

public class RetrieveCommentQuery : IRequest<Result<RetrieveCommentResponse, Success, Error>>
{
    public required Guid Id { get; init; }
}
