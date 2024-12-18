﻿using AutoMapper;
using DreamLuso.Application.Common;
using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.Application.CQ.Users.Commands.CreateUser;
public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<CreateUserResponse, Success, Error>>
{
    public async Task<Result<CreateUserResponse, Success, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
       try
        {
            var existingUser = await unitOfWork.AccountRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
                return Error.ExistingUser;

            var protectionKeys = unitOfWork.DataProtectionService.Protect(request.Password);

            var newUser = new User
            {
                Name = new Name
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName
                },
                ImageUrl = request.ImageUrl ?? string.Empty,
                PhoneNumber = request.PhoneNumber ?? string.Empty,
                DateOfBirth = request.DateOfBirth,
            };

            await unitOfWork.UserRepository.AddAsync(newUser, cancellationToken);
            await unitOfWork.CommitAsync();

            var newAccount = new Account
            {
                Email = request.Email,
                PasswordHash = protectionKeys.PasswordHash,
                PasswordSalt = protectionKeys.PasswordSalt,
                UserId = newUser.Id,
            };

            await unitOfWork.AccountRepository.AddAsync(newAccount, cancellationToken);
            await unitOfWork.CommitAsync();

            //VER SE E NECESSARIO 
            var token = unitOfWork.TokenService.GenerateToken(newUser);

            return Success.Ok;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");

            return Error.NotFound;
        }
    }
}
