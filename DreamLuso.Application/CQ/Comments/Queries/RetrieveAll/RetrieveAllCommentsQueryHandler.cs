using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Comments.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Comments.Queries.RetrieveAll;

public class RetrieveAllCommentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllCommentsQuery, Result<RetrieveAllCommentsResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllCommentsResponse, Success, Error>> Handle(RetrieveAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await unitOfWork.CommentRepository.RetrieveAllAsync();

        if (comments == null || !comments.Any())
            return Error.CommentNotFound;

        var commitResponses = comments.Select(comment => new RetrieveCommentResponse
        {
            Id = comment.Id,
            PropertyId = comment.PropertyId,
            UserId = comment.UserId,
            Message = comment.Message,
            DateTimePosted = comment.DateTimePosted,
            Flagged = comment.Flagged
        }).ToList();

        var response = new RetrieveAllCommentsResponse 
        {
            Comments = commitResponses 
        };
            return response;

    }
}