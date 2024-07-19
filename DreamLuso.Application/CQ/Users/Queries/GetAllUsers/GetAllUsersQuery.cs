using DreamLuso.Application.Common.Responses;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<Result<GetAllUsersResponse, Success, Error>>
{
}
