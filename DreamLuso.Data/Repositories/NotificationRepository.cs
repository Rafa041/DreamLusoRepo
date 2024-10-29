using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repository;

public class NotificationRepository : PaginatedRepository<Notification, Guid>, INotificationRepository
{
    protected readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId, bool unreadOnly)
    {
        var query = _context.Notifications
            .Include(n => n.SenderUser)
            .Where(n => n.SenderId == userId);

        if (unreadOnly)
        {
            query = query.Where(n => n.Status == NotificationStatus.Unread);
        }

        return await query.ToListAsync();
    }


    public Task<IEnumerable<Notification>> GetRecentNotificationsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
