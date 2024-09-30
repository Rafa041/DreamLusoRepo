namespace DreamLuso.Application.CQ.Comments.Commands.UpdateComment;

public class UpdateCommentResponse
{
    public Guid CommentId { get; set; }
    public string? Message { get; set; }
    public int? Rating { get; set; }
    public bool? Flagged { get; set; }
    public DateTime DateTimeUpdated { get; set; }
}
