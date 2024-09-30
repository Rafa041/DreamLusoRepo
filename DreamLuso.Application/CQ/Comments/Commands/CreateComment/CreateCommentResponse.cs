namespace DreamLuso.Application.CQ.Comments.Commands.CreateComment;

public class CreateCommentResponse
{
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; }
    public DateTime DateTimePosted { get; set; }
}
