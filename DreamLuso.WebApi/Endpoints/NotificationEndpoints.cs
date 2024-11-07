using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Notifications.Commands.MarkNotificationAsReadCommand;
using DreamLuso.Application.CQ.Notifications.Queries.GetUnreadNotificationsQuery;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DreamLuso.WebApi.Endpoints;
public static class NotificationEndpoints
{
    public static void RegisterNotificationEndpoints(this IEndpointRouteBuilder routes)
    {
        var notifications = routes.MapGroup("api/notifications");

        notifications.MapGet("/{userId}/unread", Queries.GetUnreadNotifications);
        notifications.MapPut("/{id}/markAsRead", Commands.MarkAsRead);
    }

    private static class Queries
    {
        public static async Task<Results<Ok<IEnumerable<NotificationResponse>>, BadRequest<Error>>> GetUnreadNotifications(
            [FromServices] ISender sender,
            Guid userId)
        {
            var query = new GetUnreadNotificationsQuery(userId);
            var result = await sender.Send(query);

            return result.IsSuccess
                ? TypedResults.Ok(result.Value)
                : TypedResults.BadRequest(result.Error);
        }
    }

    private static class Commands
    {
        public static async Task<Results<Ok, BadRequest<Error>>> MarkAsRead(
            [FromServices] ISender sender,
            Guid id)
        {
            var command = new MarkNotificationAsReadCommand(id);
            var result = await sender.Send(command);

            return result.IsSuccess
                ? TypedResults.Ok()
                : TypedResults.BadRequest(result.Error);
        }
    }
}