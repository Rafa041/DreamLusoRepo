using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class Client : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public bool IsBuyer { get; set; }
    public bool IsTenant { get; set; }
    public DateTime? DateOfFirstTransaction { get; set; }
    public Client() { }
    public Client(Guid userId, User user, bool isBuyer, bool isTenant)
    {
        UserId = userId;
        IsBuyer = isBuyer;
        IsTenant = isTenant;
    }
}