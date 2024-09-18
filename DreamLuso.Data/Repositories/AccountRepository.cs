﻿using DreamLuso.Data.Context;
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
        var accounts = await _dbSet.ToListAsync();
        var account = accounts.FirstOrDefault(a => a.Email == email);
        return account;
    }
    public async Task<Account> GetByUserIdAsync(Guid userId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(u => u.UserId == userId);
    }
}
