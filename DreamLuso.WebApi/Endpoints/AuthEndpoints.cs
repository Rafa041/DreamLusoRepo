using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Accounts.Commands.UpdateAccount;
using DreamLuso.Application.CQ.Accounts.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class AuthEndpoints
{
    public static void RegisterAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var auth = routes.MapGroup("api/auth");

        // Endpoint de Login
        auth.MapPost("/login", Queries.LoginUser);

        // Endpoint de Atualização de Senha
        auth.MapPut("/{id}", Queries.UpdateAccountPassword);
    }

    private static class Queries
    {
        // Endpoint para realizar o login
        public static async Task<Results<Ok<LoginUserResponse>, BadRequest<Error>>> LoginUser(
            [FromServices] ISender sender,
            [FromBody] LoginUserQuery command)
        {
            if (command == null)
            {
                return TypedResults.BadRequest(Error.InvalidCredentials);
            }

            var result = await sender.Send(command);

            return result.IsSuccess  ? TypedResults.Ok(result.Value) : TypedResults.BadRequest(result.Error);
        }

        // Endpoint para atualizar a senha da conta
        public static async Task<Results<Ok, NotFound, BadRequest<Error>>> UpdateAccountPassword(
            [FromServices] ISender sender,
            [FromBody] UpdateAccountPasswordCommand command)
        {
            if (command == null)
            {
                return TypedResults.BadRequest(Error.InvalidCredentials);
            }

            var result = await sender.Send(command);

            return result?.IsSuccess == true ? TypedResults.Ok() : TypedResults.BadRequest(Error.NotFound);
        }
    }
}
