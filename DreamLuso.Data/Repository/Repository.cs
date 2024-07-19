using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using DreamLuso.Domain.Interface;
using System.Linq;


namespace DreamLuso.Data.Repository;
public abstract class Repository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await DbSet.AddAsync(entity, cancellationToken);

        return entityEntry.Entity;
    }

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = DbSet.Update(entity);
        return Task.FromResult(entityEntry.Entity);
    }

    public Task<T> DeleteByIdAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = DbSet.Remove(entity);
        return Task.FromResult(entityEntry.Entity);
    }
}
