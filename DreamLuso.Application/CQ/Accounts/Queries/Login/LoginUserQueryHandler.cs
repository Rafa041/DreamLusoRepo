using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Accounts.Queries.Login;

public class LoginUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserQuery, Result<LoginUserResponse, Success, Error>>
{
    public async Task<Result<LoginUserResponse, Success, Error>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {

        var existingUser = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

        if (existingUser == null)
            return Error.NotFound;


        var isPasswordValid = unitOfWork.DataProtectionService.VerifyPassword(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

        if (!isPasswordValid)
            return Error.InvalidCredentials;

        // Gera o token JWT para o usuário autenticado
        var user = await unitOfWork.UserRepository.RetrieveAsync(existingUser.UserId);
        var token = unitOfWork.TokenService.GenerateToken(user);

        var response = new LoginUserResponse
        {
            Id = user.Id,
            Token = token
        };

        return response;
    }
}
