using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Result<CreateCommentResponse, Success, Error>>
{
    public required Guid PropertyId { get; init; }
    public required Guid UserId { get; init; }
    public required string Message { get; init; }
    public required int Rating { get; init; }
    public required DateTime DateTimePosted { get; init; }
    public bool Flagged { get; init; }
}
