using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

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
}
