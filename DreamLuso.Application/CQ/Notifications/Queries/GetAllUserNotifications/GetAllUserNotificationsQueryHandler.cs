using DreamLuso.Application.Common.Responses;
using DreamLuso.Application.CQ.Notifications.Queries.GetUnreadNotificationsQuery;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Notifications.Queries.GetAllUserNotifications;
public class GetAllUserNotificationsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllUserNotificationsQuery, Result<IEnumerable<NotificationResponse>, Success, Error>>
{


    public async Task<Result<IEnumerable<NotificationResponse>, Success, Error>> Handle(GetAllUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await unitOfWork.NotificationRepository.GetAllUserNotificationsAsync(request.UserId, cancellationToken);

        if (notifications == null || !notifications.Any())
            return Error.NotificationNotFound;

        var response = notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            Message = n.Message,
            CreatedAt = n.CreatedAt,
            Type = n.Type.ToString(),
            Priority = n.Priority.ToString(),
            Status = n.Status.ToString(),
            SenderId = n.SenderId,
            RecipientId = n.RecipientId,
            SenderName = n.SenderUser != null
                ? $"{n.SenderUser.Name.FirstName} {n.SenderUser.Name.LastName}"
                : "System",
            ExpirationDate = n.ExpirationDate
        }).ToList();

        return response;
    }
}