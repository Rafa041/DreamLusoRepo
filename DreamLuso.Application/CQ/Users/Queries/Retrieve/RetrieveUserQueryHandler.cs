using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQueryHandler : IRequestHandler<RetrieveUserQuery, Result<RetrieveUserResponse, Success, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RetrieveUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RetrieveUserResponse, Success, Error>> Handle(RetrieveUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.RetrieveAsync(request.Id);

        if (user == null)
            return Error.NotFound;

        var response = new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Account.Email,
            Access = user.Access,
            ImageUrl = user.ImageUrl,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        };

        return response;
    }
}
