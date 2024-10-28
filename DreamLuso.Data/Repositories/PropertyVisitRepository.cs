using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repository;

public class PropertyVisitRepository : PaginatedRepository<PropertyVisit, Guid>, IPropertyVisitRepository
{
    protected readonly ApplicationDbContext _context;

    public PropertyVisitRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
