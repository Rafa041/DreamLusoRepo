using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IMessageRepository : IRepository<Message, Guid>
{
    Task<IEnumerable<Message>> GetChatMessagesAsync(Guid chatId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Message>> GetUnreadMessagesAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Message> MarkAsReadAsync(Guid messageId, CancellationToken cancellationToken = default);
}