using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IRealStateAgentRepository : IRepository<RealStateAgent, Guid>
{

    Task<RealStateAgent> RetrieveByUserIdAsync(Guid id, CancellationToken cancellationToken = default);


}