
using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;

namespace DreamLuso.Application.CQ.Users.Commands.UpdateUser;

public class UpdateUserCommandHadler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse, Success, Error>>
{
    public async Task<Result<UpdateUserResponse, Success, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.RetrieveAsync(request.Id);

        if (existingUser == null)
            return Error.NotFound;

        existingUser.Name.FirstName = request.FirstName;
        existingUser.Name.LastName = request.LastName;
        existingUser.ImageUrl = request.ImageUrl;
        existingUser.Access = request.Access;
        existingUser.PhoneNumber = request.PhoneNumber;
        existingUser.DateOfBirth = request.DateOfBirth;


        await unitOfWork.UserRepository.UpdateAsync(existingUser);

        await unitOfWork.CommitAsync();

        var token = unitOfWork.TokenService.GenerateToken(existingUser);

        return Success.Ok;
    }
}

