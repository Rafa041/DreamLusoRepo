using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IRealEstateAgentRepository : IRepository<RealEstateAgent, Guid>
{

    Task<RealEstateAgent> RetrieveByUserIdAsync(Guid id, CancellationToken cancellationToken = default);


}