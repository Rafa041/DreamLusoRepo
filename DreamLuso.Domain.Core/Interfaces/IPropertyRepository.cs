using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IPropertyRepository : IRepository<Property, Guid>
{
    Task<IEnumerable<Property>> GetAllActivePropertiesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Property>> GetAllInactivePropertiesAsync(CancellationToken cancellationToken);
    Task<decimal> GetTotalSalesForMonthAsync(int month, int year, CancellationToken cancellationToken);
}
