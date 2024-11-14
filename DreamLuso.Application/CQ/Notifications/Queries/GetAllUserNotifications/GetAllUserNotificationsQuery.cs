using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Notifications.Queries.GetUnreadNotificationsQuery;
using MediatR;


namespace DreamLuso.Application.CQ.Notifications.Queries.GetAllUserNotifications;

public record GetAllUserNotificationsQuery(Guid UserId)
    : IRequest<Result<IEnumerable<NotificationResponse>, Success, Error>>;