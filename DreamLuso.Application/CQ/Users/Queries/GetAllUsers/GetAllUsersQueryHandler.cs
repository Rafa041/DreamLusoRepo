using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequest<Result<GetAllUsersResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetAllUsersResponse>, Success, Error>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        if (users == null || !users.Any())
            return Error.NotFoundUser;

        var userResponses = users.Select(user => new GetAllUsersResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        }).ToList();

        //var response = new GetAllUsersResponse
        //{
        //    Users = userResponses
        //};

        return userResponses;
    }
}
