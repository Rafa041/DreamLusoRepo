using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLuso.Application.CQ.Accounts;

public class LoginUserCommand : IRequest<Result<LoginUserResponse, Success, Error>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
public class LoginUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserCommand, Result<LoginUserResponse, Success, Error>>
{
    public async Task<Result<LoginUserResponse, Success, Error>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Verificar se o usuário existe no banco de dados
        var existingUser = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

        if (existingUser == null)
            return Error.NotFound; // Retorna erro se o usuário não for encontrado

        // Verificar se a senha fornecida é válida
        var isPasswordValid = unitOfWork.DataProtectionService.VerifyPassword(request.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

        if (!isPasswordValid)
            return Error.InvalidCredentials; // Retorna erro se a senha não for válida

        // Gera o token JWT para o usuário autenticado
        var user = await unitOfWork.UserRepository.GetByIdAsync(existingUser.UserId);
        var token = unitOfWork.TokenService.GenerateToken(user);

        var response = new LoginUserResponse
        {
            Id = user.Id,
            Token = token,
        };

        return Success.Ok;
    }
}
public class LoginUserResponse
{
    public required Guid Id { get; set; }
    public required string Token { get; set; }
}
