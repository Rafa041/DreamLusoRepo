using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repository;

public class PropertyRepository : PaginatedRepository<Property, Guid>, IPropertyRepository
{
    protected readonly ApplicationDbContext _context;

    public PropertyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
