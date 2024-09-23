using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repository;

public class RealStateAgentRepository : PaginatedRepository<RealStateAgent, Guid>, IRealStateAgentRepository
{
    protected readonly ApplicationDbContext _context;

    public RealStateAgentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}