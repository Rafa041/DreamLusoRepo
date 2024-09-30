using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;


namespace DreamLuso.Application.CQ.Comments.Queries.Retrieve;

public class RetrieveCommentQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveCommentQuery, Result<RetrieveCommentResponse, Success, Error>>
{
    public async Task<Result<RetrieveCommentResponse, Success, Error>> Handle(RetrieveCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.CommentRepository.RetrieveAsync(request.Id, cancellationToken);
        if (comment == null)
            return Error.CommentNotFound;

        var response = new RetrieveCommentResponse
        {
            Id = comment.Id,
            PropertyId = comment.PropertyId,
            UserId = comment.UserId,
            Message = comment.Message,
            DateTimePosted = comment.DateTimePosted,
            Flagged = comment.Flagged
        };

        return response;
    }
}