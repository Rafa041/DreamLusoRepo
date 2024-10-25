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
    public async Task<IEnumerable<Property>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        var properties = await DbSet
            .Include(p => p.Address)
            .Include(p => p.Images)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return properties;
    }
    public async Task<Property> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Properties
            .Include(p => p.Address)
            .Include(p => p.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task<decimal> GetTotalRentedForMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        return await _context.Properties
            .Where(p => p.Status == PropertyStatus.Rent  // Filtro para arrendamento
                         && p.DateListed >= startOfMonth
                         && p.DateListed < endOfMonth)  // Considera apenas propriedades ativas
            .SumAsync(p => p.Price, cancellationToken);  // Soma o preço do arrendamento
    }

    public async Task<decimal> GetTotalSalesForMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        var startOfMonth = new DateTime(year, month, 1);
        var endOfMonth = startOfMonth.AddMonths(1);

        return await _context.Properties
            .Where(p => p.Status == PropertyStatus.Sale  // Filtro para arrendamento
                         && p.DateListed >= startOfMonth
                         && p.DateListed < endOfMonth)  // Considera apenas propriedades ativas
            .SumAsync(p => p.Price, cancellationToken);  // Soma o preço do arrendamento
    }
}
