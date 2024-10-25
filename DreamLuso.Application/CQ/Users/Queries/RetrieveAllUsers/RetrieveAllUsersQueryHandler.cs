using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;

public class RetrieveAllUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<RetrieveAllUsersQuery, Result<RetrieveAllUsersResponse, Success, Error>>
{
    public async Task<Result<RetrieveAllUsersResponse, Success, Error>> Handle(RetrieveAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.RetrieveAllAsync();

        if (users == null || !users.Any())
            return Error.NotFound;

        var userResponses = users.Select(user => new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            Access = user.Access.ToString(),
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        }).ToList();

        var response = new RetrieveAllUsersResponse
        {
            Users = userResponses
        };

        return response;
    }
}