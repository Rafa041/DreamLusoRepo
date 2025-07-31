using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IPropertyVisitRepository : IRepository<PropertyVisit, Guid>
{
    Task<IEnumerable<PropertyVisit>> RetrieveAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<PropertyVisit> RetrieveByUserIdSingleAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyVisit>> GetVisitsByDateAndProperty(Guid propertyId, string date, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyVisit>> RetrieveAllByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<PropertyVisit> RetrieveByTokenAsync(string token);
    Task<bool> IsTimeSlotAvailable(Guid propertyId, DateOnly visitDate, TimeSlot timeSlot, CancellationToken cancellationToken);
    Task ConfirmVisitAsync(Guid visitId, CancellationToken cancellationToken);
    Task CancelVisitAsync(Guid visitId, CancellationToken cancellationToken);
    Task<IEnumerable<PropertyVisit>> RetrieveByRealEstateAgentIdSingleAsync(Guid realEstateAgentId, CancellationToken cancellationToken);
}