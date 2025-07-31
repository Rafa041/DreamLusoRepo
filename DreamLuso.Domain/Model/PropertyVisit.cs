using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;

namespace DreamLuso.Domain.Model;

public class PropertyVisit : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RealEstateAgentId { get; set; }
    public RealEstateAgent RealEstateAgentUser { get; set; }
    public DateOnly VisitDate { get; set; }
    public TimeSlot TimeSlot { get; set; }
    public VisitStatus VisitStatus { get; set; }
    public string? Notes { get; set; }
    public string ConfirmationToken { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }

    public PropertyVisit()
    {
        GenerateConfirmationToken();
    }

    public PropertyVisit(
        Guid id,
        Property property,
        Guid propertyId,
        User user,
        Guid userId,
        RealEstateAgent realEstateUser,
        Guid realtorUserId,
        DateOnly visitDate,
        TimeSlot timeSlot,
        VisitStatus visitStatus)
    {
        Id = id;
        Property = property;
        PropertyId = propertyId;
        User = user;
        UserId = userId;
        RealEstateAgentUser = realEstateUser;
        RealEstateAgentId = realtorUserId;
        VisitDate = visitDate;
        TimeSlot = timeSlot;
        VisitStatus = visitStatus;
        GenerateConfirmationToken();
    }

    private void GenerateConfirmationToken()
    {
        ConfirmationToken = $"VISIT-{Guid.NewGuid():N}";
    }

    public void Confirm()
    {
        VisitStatus = VisitStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
    }

    public void Cancel() => VisitStatus = VisitStatus.Canceled;
}

public enum TimeSlot
{
    Morning_8AM_10AM,
    Morning_10AM_12AM,
    Afternoon_2PM_4PM,
    Afternoon_4PM_6PM

}

public enum VisitStatus
{
    Scheduled,
    Completed,
    Pending,
    Canceled,
    Confirmed
}