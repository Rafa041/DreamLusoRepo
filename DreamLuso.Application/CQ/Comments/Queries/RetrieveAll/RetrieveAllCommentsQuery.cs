using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Comments.Queries.RetrieveAll;

public class RetrieveAllCommentsQuery : IRequest<Result<RetrieveAllCommentsResponse, Success, Error>>
{
}
