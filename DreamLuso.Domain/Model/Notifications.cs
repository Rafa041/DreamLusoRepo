using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Notifications : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public User SenderUser { get; set; }
    public Guid RecipientId { get; set; }
    public User RecipentUser { get; set; }
    public string Message { get; set; }
    public DateTime CreateAt { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public DateTime ExpirationDate { get; set; }

    // - Prioridade da notificação (alta, média, baixa)
    public NotificationPriority Priority { get; set; }
    public Notifications() { }


    public Notifications(
        Guid senderId,
        Guid recipientId,
        string message,
        NotificationType type,
        NotificationPriority priority,
        string referenceType = null) : this()
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Message = message;
        Type = type;
        Priority = priority;
        Type = type;
        ExpirationDate = CalculateExpirationDate(type, priority);
    }
    public void MarkAsRead()
    {
        Status = NotificationStatus.Read;
    }
    private DateTime CalculateExpirationDate(NotificationType type, NotificationPriority priority)
    {
        return type switch
        {
            NotificationType.Payment => DateTime.UtcNow.AddMonths(6),
            NotificationType.ContractUpdate => DateTime.UtcNow.AddMonths(3),
            NotificationType.PropertyUpdate => DateTime.UtcNow.AddDays(30),
            _ => DateTime.UtcNow.AddDays(7)
        };
    }
}

public enum NotificationStatus
{
    Unread,
    Read,
    Archived,
    Deleted
}
public enum NotificationType
{
    Payment,
    Contract,
    ContractUpdate,
    PropertyUpdate,
    PropertyViewing,
    NewListing,
    PriceChange,
    DocumentUpload,
    SystemAlert,
    Message,
    Favorite,
    Visit
}

public enum NotificationPriority
{
    High,
    Medium,
    Low
}

