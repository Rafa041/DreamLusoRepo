using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Users.Commands.CreateUser;
using DreamLuso.Application.CQ.Users.Commands.UpdateUser;
using DreamLuso.Application.CQ.Users.Queries.Retrieve;
using DreamLuso.Application.CQ.Users.Queries.RetrieveAllUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void RegisterUserEndpoints(this IEndpointRouteBuilder routes)
    {
        //var users = routes.MapGroup("api/user").RequireAuthorization();
        var users = routes.MapGroup("api/user");
        // ========== Register User Endpoint ==========
        users.MapPost("/register", Queries.RegisterUser)
            .WithName("RegisterUser")
            .Produces<CreateUserResponse>(200)
            .Produces<Error>(400)
             .DisableAntiforgery();//Em teste 

        // ========== Retrieve All Users Endpoint ==========
        users.MapGet("/retrieveall", Queries.RetrieveAllUsers)
            .WithName("RetrieveAllUsers")
            .Produces<List<RetrieveUserResponse>>(200)
            .Produces<Error>(404);

        // ========== Update User Endpoint ==========
        users.MapPut("/{id:guid}", Queries.UpdateUser)
            .WithName("UpdateUser")
            .Produces<UpdateUserResponse>(200)
            .Produces<Error>(404);

        // ========== Retrieve User by Id Endpoint ==========
        users.MapGet("/{id:guid}", Queries.RetrieveUserById)
            .WithName("RetrieveUser")
            .Produces<RetrieveUserResponse>(200)
            .Produces<Error>(404)
            .DisableAntiforgery();
    }

    private static class Queries
    {
        public static async Task<Results<Ok<bool>, BadRequest<Error>>> RegisterUser(
      [FromServices] ISender sender,
     [FromForm] CreateUserCommand command,
    CancellationToken cancellationToken) 
        {
            
            if (command.ImageFile != null && command.ImageFile.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagesUsers");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(command.ImageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await command.ImageFile.CopyToAsync(stream, cancellationToken);
                }

                command.ImageUrl = $"/ImagesUsers/{fileName}";
            }

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? TypedResults.Ok(result.IsSuccess)
                : TypedResults.BadRequest(result.Error);
        }
        // ========== Retrieve All Users ==========
        public static async Task<Results<Ok<IEnumerable<RetrieveUserResponse>>, NotFound<Error>>> RetrieveAllUsers(
            [FromServices] ISender sender,
            CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RetrieveAllUsersQuery(), cancellationToken);
            return !result.IsSuccess ? TypedResults.NotFound(result.Error) : TypedResults.Ok(result.Value.Users);
        }

        // ========== Update User ==========
        public static async Task<Results<Ok<bool>, NotFound<string>>> UpdateUser(
            [FromServices] ISender sender,
            [FromBody] UpdateUserCommand command)
        {
            var result = await sender.Send(command);
            return result.IsSuccess ? TypedResults.Ok(result.IsSuccess) : TypedResults.NotFound("User not found.");
        }

        // ========== Retrieve User by Id ==========
        public static async Task<Results<Ok<RetrieveUserResponse>, NotFound<Error>>> RetrieveUserById(
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new RetrieveUserQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);
            return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound(result.Error);
        }
    }
}
