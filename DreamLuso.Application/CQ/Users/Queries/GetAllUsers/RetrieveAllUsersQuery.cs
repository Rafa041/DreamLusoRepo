using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.GetAllUsers;

public class RetrieveAllUsersQuery : IRequest<Result<RetrieveAllUsersResponse, Success, Error>>
{
}
