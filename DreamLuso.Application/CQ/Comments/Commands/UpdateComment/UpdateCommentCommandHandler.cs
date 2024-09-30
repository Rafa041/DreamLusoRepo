using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Comments.Commands.UpdateComment;

public class UpdateCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommentCommand, Result<UpdateCommentResponse, Success, Error>>
{
    public async Task<Result<UpdateCommentResponse, Success, Error>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {

        var comment = await unitOfWork.CommentRepository.RetrieveAsync(request.CommentId);

        if (comment == null)
            return Error.CommentNotFound;

       
        if (!string.IsNullOrWhiteSpace(request.Message))
            comment.Message = request.Message;

        if (request.Rating.HasValue)
            comment.Rating = request.Rating.Value;

        if (request.Flagged.HasValue)
            comment.Flagged = request.Flagged.Value;

        comment.DateTimePosted = DateTime.UtcNow; // Atualiza a data de modificação


        await unitOfWork.CommentRepository.UpdateAsync(comment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new UpdateCommentResponse
        {
            CommentId = comment.Id,
            Message = comment.Message,
            Rating = comment.Rating,
            Flagged = comment.Flagged,
            DateTimeUpdated = comment.DateTimePosted
        };

        return response;
    }
}
