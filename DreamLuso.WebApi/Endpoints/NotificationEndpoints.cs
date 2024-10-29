using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Notifications.Commands.Create;
using DreamLuso.Application.CQ.Notifications.Queries.GetUserNotification;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;

public static class NotificationEndpoints
{
    public static void RegisterNotificationEndpoints(this IEndpointRouteBuilder routes)
    {
        var notification = routes.MapGroup("api/notifications");

        // Endpoint para adicionar notificação
        notification.MapPost("/", Commands.AddNotification);

        // Endpoint para recuperar notificações de um usuário específico
        notification.MapGet("/user/{userId}", Queries.GetUserNotifications); // Lembrar disso quando nao funcionar 
    }

    private static class Commands
    {
        // Endpoint para adicionar notificação
        public static async Task<Results<Ok, BadRequest<Error>>> AddNotification(
            [FromServices] ISender sender,
            [FromBody] CreateNotificationCommand command)
        {
            if (command == null)
            {
                return TypedResults.BadRequest(Error.InvalidNotificationData);
            }

            var result = await sender.Send(command);

            return result?.IsSuccess == true
          ? TypedResults.Ok()
          : TypedResults.BadRequest(result?.Error);
        }
    }

    private static class Queries
    {
        // Endpoint para recuperar notificações de um usuário
        public static async Task<Results<Ok<List<GetUserNotificationsQueryResponse>>, BadRequest<Error>>> GetUserNotifications(
            [FromServices] ISender sender,
            [FromRoute] Guid userId,
            [FromQuery] bool unreadOnly = false)
        {
            var query = new GetUserNotificationsQuery
            {
                UserId = userId,
                UnreadOnly = unreadOnly
            };

            var result = await sender.Send(query);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Error);
        }
    }
}
