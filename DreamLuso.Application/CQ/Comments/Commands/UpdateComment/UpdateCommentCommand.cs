using DreamLuso.Application.Common.Responses;
using MediatR;


namespace DreamLuso.Application.CQ.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest<Result<UpdateCommentResponse, Success, Error>>
{
    public required Guid CommentId { get; init; }
    public string? Message { get; init; }
    public int? Rating { get; init; }
    public bool? Flagged { get; init; }
}
