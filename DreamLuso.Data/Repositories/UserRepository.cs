using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace DreamLuso.Data.Repositories;

public class UserRepository : PaginatedRepository<User, Guid>, IUserRepository
{
    protected readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
       _context = context;
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Account.Email == email);
    }

    public async Task<IEnumerable<User>> RetrieveAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.Account)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> RetrieveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.Account)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}
