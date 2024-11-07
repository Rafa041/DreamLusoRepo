using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repositories;

public class ContractRepository : PaginatedRepository<Contract, Guid>, IContractRepository
{
    protected readonly ApplicationDbContext _context;

    public ContractRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
