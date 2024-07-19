using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace DreamLuso.Data.Repository;

public class AccountRepository : PaginatedRepository<Account, Guid> , IAccountRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly Microsoft.EntityFrameworkCore.DbSet<Account> _dbSet;

    public AccountRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Account>();
    }

    public async Task<Account> GetByEmailAsync(string email)
    {
        //return await _dbSet.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        var result = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        return result;
    }
}