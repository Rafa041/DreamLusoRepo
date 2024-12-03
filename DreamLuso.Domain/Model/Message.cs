using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Message : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
    public MessageType Type { get; set; }

    public void MarkAsRead()
    {
        IsRead = true;
        // Create read confirmation notification
        var notification = new Notification(
            Guid.NewGuid(),
            SenderId,
            Chat.UserId,
            "Message read",
            NotificationType.Message,
            NotificationPriority.Low
        );
    }
}
public enum MessageType
{
    Text,
    Image,
    Document,
    PropertyShare,
    VisitRequest,
    Location,
    Contact,
    Availability,
    PriceQuote,
    ContractProposal
}