using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;

public class RetrieveAllUsersQuery : IRequest<Result<RetrieveAllUsersResponse, Success, Error>>
{
}
