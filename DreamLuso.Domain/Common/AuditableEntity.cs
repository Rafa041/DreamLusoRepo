using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Common;

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdateAt { get; set; }
    public string UpdatedBy { get; set; }
}
