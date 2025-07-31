using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IChatRepository : IRepository<Chat, Guid>
{
    Task<IEnumerable<Chat>> RetrieveAllAsync(CancellationToken cancellationToken = default);
    Task<Chat> RetrieveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Chat>> GetUserChatsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Chat>> GetRealEstateAgentChatsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Chat> UpdateStatusAsync(Guid id, ChatStatus status, CancellationToken cancellationToken = default);
}