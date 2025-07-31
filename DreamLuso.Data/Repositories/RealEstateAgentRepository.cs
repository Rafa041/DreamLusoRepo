using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repositories;

public class RealEstateAgentRepository : PaginatedRepository<RealEstateAgent, Guid>, IRealEstateAgentRepository
{
    protected readonly ApplicationDbContext _context;

    public RealEstateAgentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RealEstateAgent> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)
            .ThenInclude(u => u.Account)
            .FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);
    }
    public async Task<RealEstateAgent> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)
            .ThenInclude(u => u.Account)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<RealEstateAgent>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)           // Includes the related User entity
            .ThenInclude(u => u.Account)    // Further includes the Account entity related to User
            .ToListAsync(cancellationToken);
    }
}