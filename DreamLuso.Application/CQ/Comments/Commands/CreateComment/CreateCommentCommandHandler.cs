using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, Result<CreateCommentResponse, Success, Error>>
{
    public async Task<Result<CreateCommentResponse, Success, Error>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.UserId);

        if (existingUser == null)
            return Error.UserNotFound;

        var comment = new Comment
        {
            PropertyId = request.PropertyId,
            UserId = request.UserId,
            Message = request.Message,
            Rating = request.Rating,
            DateTimePosted = DateTime.Now
        };

        await unitOfWork.CommentRepository.AddAsync(comment, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var response = new CreateCommentResponse
        {
            PropertyId = comment.PropertyId,
            UserId = comment.UserId,
            Message = comment.Message,
            Rating = comment.Rating,
            DateTimePosted = comment.DateTimePosted
        };
        return response;
    }
}