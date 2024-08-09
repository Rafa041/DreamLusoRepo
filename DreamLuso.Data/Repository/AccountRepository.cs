using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

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
        //var result = await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        //return result;
        // Carrega todos os registros de Account na memória
        var accounts = await _dbSet.ToListAsync();

        // Compara cada registro com o email fornecido
        var account = accounts.FirstOrDefault(a => a.Email == email);

        return account;
    }
    //public async Task<Account> GetByEmailAsync(string email)// Em teste
    //{
    //    if (!(_dbSet is IQueryable<Account> queryable && queryable.Provider is IDbAsyncQueryProvider))
    //    {
    //        throw new InvalidOperationException("The provider for the source IQueryable doesn't implement IDbAsyncQueryProvider.");
    //    }

    //    var result = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    //    return result;
    //}
}