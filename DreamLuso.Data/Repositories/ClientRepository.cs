using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repositories;

public class ClientRepository : PaginatedRepository<Client, Guid>, IClientRepository
{
    protected readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
