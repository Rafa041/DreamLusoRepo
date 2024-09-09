using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IAddressRepository : IRepository<Address, Guid>
{
    Task<Address> GetByFullAddressAsync(string street, string city, string state, string postalCode, string country);
}