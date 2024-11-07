using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface INotificationRepository : IRepository<Notification, Guid>
{
    Task<IEnumerable<Notification>> GetNotificationsAsync(Guid userId);
    Task<IEnumerable<Notification>> GetRecentNotificationsAsync(Guid userId);
    Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken);
    Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId, bool unreadOnly);
}