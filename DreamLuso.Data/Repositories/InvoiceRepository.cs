using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repository;

public class InvoiceRepository : PaginatedRepository<Invoice, Guid>, IInvoiceRepository
{
    protected readonly ApplicationDbContext _context;

    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
