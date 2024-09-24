using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Client : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsActive { get; set; } = true;
    public Client() { }
    public Client(Guid userId, User user, bool isActive)
    {
        UserId = userId;
        IsActive = isActive;
    }
}