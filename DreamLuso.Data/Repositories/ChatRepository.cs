using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace DreamLuso.Data.Repositories;

public class ChatRepository : PaginatedRepository<Chat, Guid>, IChatRepository
{
    protected readonly ApplicationDbContext _context;

    public ChatRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Chat>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Property)
            .Include(c => c.User)
            .Include(c => c.RealStateAgent)
            .Include(c => c.Messages)
            .ToListAsync(cancellationToken);
    }

    public async Task<Chat> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Property)
            .Include(c => c.User)
            .Include(c => c.RealStateAgent)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Chat>> GetUserChatsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Property)
            .Include(c => c.Messages)
            .Include(c => c.User)
            .Include(c => c.RealStateAgent)
                .ThenInclude(r => r.User)  
            .Where(c => c.UserId == userId || c.RealStateAgentId == userId)
            .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<Chat>> GetRealStateAgentChatsAsync(Guid realStateAgentId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(c => c.Property)
            .Include(c => c.Messages)
            .Include(c => c.User)
            .Include(c => c.RealStateAgent)
                .ThenInclude(r => r.User)
            .Where(c => c.RealStateAgentId == realStateAgentId)
            .ToListAsync(cancellationToken);
    }
    public async Task<Chat> UpdateStatusAsync(Guid id, ChatStatus status, CancellationToken cancellationToken = default)
    {
        var chat = await RetrieveAsync(id, cancellationToken);
        if (chat != null)
        {
            chat.Status = status;
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return chat;
    }
}