using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repositories;

public class NotificationRepository : PaginatedRepository<Notification, Guid>, INotificationRepository
{
    protected readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Include(n => n.SenderUser)
            .Where(n => n.RecipientId == userId && n.Status == NotificationStatus.Unread)
            .OrderByDescending(n => n.CreateAt)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsReadAsync(Guid notificationId, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId, cancellationToken);

        if (notification != null)
        {
            notification.MarkAsRead();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId, bool unreadOnly)
    {
        var query = _context.Notifications
            .Include(n => n.SenderUser)
            .Where(n => n.RecipientId == userId);

        if (unreadOnly)
        {
            query = query.Where(n => n.Status == NotificationStatus.Unread);
        }

        return await query
            .OrderByDescending(n => n.CreateAt)
            .ToListAsync();
    }
    public async Task<IEnumerable<Notification>> GetAllUserNotificationsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Include(n => n.SenderUser)
            .Where(n => n.RecipientId == userId)
            .OrderByDescending(n => n.CreateAt)
            .ToListAsync(cancellationToken);
    }

    public Task<IEnumerable<Notification>> GetNotificationsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Notification>> GetRecentNotificationsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
