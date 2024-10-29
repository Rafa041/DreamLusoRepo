using DreamLuso.Application.Common.Responses;
using DreamLuso.Domain.Core.Interfaces;
using MediatR;

namespace DreamLuso.Application.CQ.Notifications.Queries.GetUserNotification;

public class GetUserNotificationsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserNotificationsQuery, Result<List<GetUserNotificationsQueryResponse>, Success, Error>>
{

    public async Task<Result<List<GetUserNotificationsQueryResponse>, Success, Error>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        // Fetch notifications from repository with filtering
        var notifications = await unitOfWork.NotificationRepository.GetUserNotificationsAsync(request.UserId, request.UnreadOnly);

        if (notifications == null || !notifications.Any())
            return Error.NotificationNotFound;

        // Map notifications to the response DTO
        var response = notifications.Select(n => new GetUserNotificationsQueryResponse
        {
            Id = n.Id,
            Message = n.Message,
            CreateAt = n.CreatedAt,
            Status = n.Status,
            Type = n.Type,
            Priority = n.Priority,
            SenderName = $"{n.SenderUser?.Name?.FirstName ?? string.Empty} {n.SenderUser?.Name?.LastName ?? string.Empty}".Trim()
        }).ToList();

        return response;
    }
}