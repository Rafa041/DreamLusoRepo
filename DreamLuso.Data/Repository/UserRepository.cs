using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data.Entity;

namespace DreamLuso.Data.Repository;

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
}
