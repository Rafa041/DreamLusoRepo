using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IAddressRepository : IRepository<Address, Guid>
{
    Task<Address> GetByFullAddressAsync(Address address, CancellationToken cancellationToken = default);
}