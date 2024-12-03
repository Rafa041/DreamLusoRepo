using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace DreamLuso.Data.Repositories;

public class MessageRepository : PaginatedRepository<Message, Guid>, IMessageRepository
{
    protected readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetChatMessagesAsync(Guid chatId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.Chat)
                .ThenInclude(c => c.User)
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.Chat)
            .Where(m => !m.IsRead && m.Chat.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Message> MarkAsReadAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        var message = await DbSet.FindAsync(messageId);
        if (message != null)
        {
            message.IsRead = true;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return message;
    }
}