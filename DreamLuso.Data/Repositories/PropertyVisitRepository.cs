using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repositories;


public class PropertyVisitRepository : PaginatedRepository<PropertyVisit, Guid>, IPropertyVisitRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<PropertyVisit> _dbSet;

    public PropertyVisitRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<PropertyVisit>();
    }
    public async Task<bool> IsTimeSlotAvailable(Guid propertyId, DateOnly visitDate, TimeSlot timeSlot, CancellationToken cancellationToken)
    {
        return !await _context.PropertyVisits
            .Where(v =>
                v.PropertyId == propertyId &&
                v.VisitDate == visitDate &&
                v.TimeSlot == timeSlot &&
                v.VisitStatus != VisitStatus.Canceled)
            .AnyAsync(cancellationToken);

    }
    public async Task<IEnumerable<PropertyVisit>> RetrieveAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Property)
            .Include(pv => pv.User)
            .Include(pv => pv.RealEstateAgentUser)
            .Where(pv => pv.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyVisit> RetrieveByUserIdSingleAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Property)
            .Include(pv => pv.User)
            .Include(pv => pv.RealEstateAgentUser)
            .FirstOrDefaultAsync(pv => pv.UserId == userId, cancellationToken);
    }
    public async Task<IEnumerable<PropertyVisit>> RetrieveByRealEstateAgentIdSingleAsync(Guid realEstateAgentId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(pv => pv.Property)
            .Include(pv => pv.User)
            .Include(pv => pv.RealEstateAgentUser)
            .Where(pv => pv.RealEstateAgentId == realEstateAgentId)
            .ToListAsync(cancellationToken);
    }
    public async Task<IEnumerable<PropertyVisit>> RetrieveAllAsync(CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Include(v => v.Property)
            .Include(v => v.User)
            .Include(v => v.RealEstateAgentUser)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyVisit>> GetVisitsByDateAndProperty(Guid propertyId, string date, CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Where(visit =>
                visit.PropertyId == propertyId &&
                visit.VisitDate.ToString() == date &&
                visit.VisitStatus != VisitStatus.Canceled)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyVisit>> RetrieveAllByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.PropertyVisits
            .Include(v => v.Property)
            .Include(v => v.User)
                .ThenInclude(u => u.Name)
            .Include(v => v.RealEstateAgentUser)
                .ThenInclude(u => u.User)
            .Where(v => v.UserId == userId || v.RealEstateAgentId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyVisit> RetrieveByTokenAsync(string token)
    {
        return await _dbSet
            .Include(v => v.Property)
            .Include(v => v.User)
                .ThenInclude(u => u.Name)
            .Include(v => v.User)
                .ThenInclude(u => u.Account)
            .Include(v => v.RealEstateAgentUser)
                .ThenInclude(r => r.User)
            .Include(v => v.RealEstateAgentUser)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(v => v.ConfirmationToken == token);
    }
    public async Task ConfirmVisitAsync(Guid visitId, CancellationToken cancellationToken)
    {
        var visit = await _dbSet
            .FirstOrDefaultAsync(v => v.Id == visitId, cancellationToken);

        if (visit != null)
        {
            visit.Confirm();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task CancelVisitAsync(Guid visitId, CancellationToken cancellationToken)
    {
        var visit = await _dbSet
            .FirstOrDefaultAsync(v => v.Id == visitId, cancellationToken);

        if (visit != null)
        {
            visit.Cancel();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}