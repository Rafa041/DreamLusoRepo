using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DreamLuso.Data.Repository;

public class PropertyRepository : PaginatedRepository<Property, Guid>, IPropertyRepository
{
    protected readonly ApplicationDbContext _context;

    public PropertyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllActivePropertiesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Properties
    .Where(p => p.IsActive)
    .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<Property>> GetAllInactivePropertiesAsync(CancellationToken cancellationToken)
    {
        return await _context.Properties
            .Where(p => !p.IsActive)
            .ToListAsync(cancellationToken);
    }
    public async Task<decimal> GetTotalSalesForMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        return await _context.Properties
            .Where(p => p.IsForSale && p.Status == PropertyStatus.Sold
                         && p.DateListed >= startOfMonth
                         && p.DateListed < endOfMonth)
            .SumAsync(p => p.Price, cancellationToken);
    }
}
