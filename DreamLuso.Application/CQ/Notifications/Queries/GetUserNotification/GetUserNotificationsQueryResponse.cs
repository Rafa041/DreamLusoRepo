using DreamLuso.Domain.Model;

namespace DreamLuso.Application.CQ.Notifications.Queries.GetUserNotification;

public class GetUserNotificationsQueryResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime CreateAt { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public NotificationPriority Priority { get; set; }
    public string SenderName { get; set; }
}
