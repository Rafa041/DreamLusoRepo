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
    public DateTime Date { get; set; }
    public NotificationStatus Status { get; set; }
    public NotificationType Type { get; set; }
    public DateTime ExpirationDate { get; set; }

    // - Prioridade da notificação (alta, média, baixa)
    public NotificationPriority Priority { get; set; }

    private Notifications()
    {
        Id = Guid.NewGuid();
    }
    public Notifications(
        Guid senderId,
        Guid recipientId,
        string message,
        DateTime date,
        NotificationStatus status,
        NotificationType notificationType,
        DateTime expirationDate,
        NotificationPriority notificationPriority)
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Message = message;
        Date = date;
        Status = status;
        Type = notificationType;
        ExpirationDate = expirationDate;
        Priority = notificationPriority;

    }
}

public enum NotificationStatus
{
    Unread,
    Read,
    Acknowledged,
    Pending
}
public enum NotificationType
{
    Alert,
    Message,
    Update,
    Payment
}

public enum NotificationPriority
{
    High,
    Medium,
    Low
}

