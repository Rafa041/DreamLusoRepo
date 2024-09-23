using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repository;

public class AddressRepository : PaginatedRepository<Address, Guid>, IAddressRepository
{
    protected readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Address> GetByFullAddressAsync(Address address, CancellationToken cancellationToken)
    {

        return await _context.Addresses
            .Where(a => a.Street == address.Street &&
                        a.City == address.City &&
                        a.State == address.State &&
                        a.PostalCode == address.PostalCode &&
                        a.Country == address.Country)
            .FirstOrDefaultAsync(cancellationToken);
    }

}
public class ClientRepository : PaginatedRepository<Client, Guid>, IClientRepository
{
    protected readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}