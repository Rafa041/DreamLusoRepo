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
    public async Task<Address> GetByFullAddressAsync(string street, string city, string state, string postalCode, string country)
    {

        return await _context.Addresses
            .Where(a => a.Street == street &&
                        a.City == city &&
                        a.State == state &&
                        a.PostalCode == postalCode &&
                        a.Country == country)
            .FirstOrDefaultAsync();
    }

}
