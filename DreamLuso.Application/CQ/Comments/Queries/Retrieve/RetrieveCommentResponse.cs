namespace DreamLuso.Application.CQ.Comments.Queries.Retrieve;

public class RetrieveCommentResponse
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime DateTimePosted { get; set; }
    public bool Flagged { get; set; }
}
