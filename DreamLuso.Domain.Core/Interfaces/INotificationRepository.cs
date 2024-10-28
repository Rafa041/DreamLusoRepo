using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface INotificationRepository : IRepository<Notifications, Guid>
{
    Task<IEnumerable<Notifications>> GetNotificationsAsync(Guid userId);
    Task<IEnumerable<Notifications>> GetRecentNotificationsAsync(Guid userId);
    Task<IEnumerable<Notifications>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken);
    Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken);
}