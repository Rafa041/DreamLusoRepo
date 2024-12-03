using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Chat : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public List<Message> Messages { get; set; } = new();
    public ChatStatus Status { get; set; }
    public DateTime LastMessageAt { get; set; }

    public void SendMessage(Message message)
    {
        Messages.Add(message);
        LastMessageAt = DateTime.UtcNow;

        // Create notification for recipient
        var notification = new Notification(
            Guid.NewGuid(),
            message.SenderId,
            message.SenderId == UserId ? RealStateAgentId : UserId,
            $"New message about {Property.Title}",
            NotificationType.Message,
            NotificationPriority.Medium
        );
    }
}
public enum ChatStatus
{
    Active,
    Archived,
    Closed,
    Pending,
    Blocked
}