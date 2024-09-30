using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repository;

public class RealStateAgentRepository : PaginatedRepository<RealStateAgent, Guid>, IRealStateAgentRepository
{
    protected readonly ApplicationDbContext _context;

    public RealStateAgentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<RealStateAgent> GetByUserIdAsync(Guid id)
    {
        var realStates = await _context.RealStateAgent.ToListAsync();
        var real = realStates.FirstOrDefault(a => a.UserId == id);
        return real;
    }
}