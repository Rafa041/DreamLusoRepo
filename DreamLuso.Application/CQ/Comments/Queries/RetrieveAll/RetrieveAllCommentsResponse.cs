using DreamLuso.Application.CQ.Comments.Queries.Retrieve;

namespace DreamLuso.Application.CQ.Comments.Queries.RetrieveAll;

public class RetrieveAllCommentsResponse
{
    public IEnumerable<RetrieveCommentResponse> Comments { get; set; }
}
