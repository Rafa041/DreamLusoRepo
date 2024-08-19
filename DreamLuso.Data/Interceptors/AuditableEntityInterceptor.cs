using DreamLuso.Domain.Common;
using DreamLuso.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DreamLuso.Data.Interceptors;


internal class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IUserResolverService _userResolverService;

    public AuditableEntityInterceptor(IUserResolverService userResolverService)
    {
        _userResolverService = userResolverService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
        if (context == null) return;

        var userId = _userResolverService.GetUserId().ToString() ?? "system";

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = "system";
                entry.Entity.UpdateAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedBy = userId;
                entry.Entity.UpdateAt = DateTime.UtcNow;
            }
        }
    }
}
