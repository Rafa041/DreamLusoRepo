using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
namespace DreamLuso.Domain.Model;

public class Comment : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; }
    public DateTime DateTimePosted { get; set; }

    // - Indica se o comentário foi sinalizado como inapropriado
    public bool Flagged { get; set; }

    public Comment() { }
    public Comment(
        Guid id,
        Guid propertyId,
        Guid userId,
        string message,
        int rating,
        DateTime dateTimePosted,
        bool flagged
        )
    {
        Id = id;
        PropertyId = propertyId;
        UserId = userId;
        Message = message;
        Rating = rating;
        DateTimePosted = dateTimePosted;
        Flagged = flagged;

    }
}
