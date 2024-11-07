using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repositories;

public class RealStateAgentRepository : PaginatedRepository<RealStateAgent, Guid>, IRealStateAgentRepository
{
    protected readonly ApplicationDbContext _context;

    public RealStateAgentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RealStateAgent> RetrieveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)
            .ThenInclude(u => u.Account)
            .FirstOrDefaultAsync(r => r.UserId == userId, cancellationToken);
    }
    public async Task<RealStateAgent> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)
            .ThenInclude(u => u.Account)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<RealStateAgent>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(r => r.User)           // Includes the related User entity
            .ThenInclude(u => u.Account)    // Further includes the Account entity related to User
            .ToListAsync(cancellationToken);
    }
}