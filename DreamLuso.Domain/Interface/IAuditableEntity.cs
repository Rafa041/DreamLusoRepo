namespace DreamLuso.Domain.Interface;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdateAt { get; set; }
    public string UpdatedBy { get; set; }
}
