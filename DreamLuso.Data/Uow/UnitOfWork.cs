using DreamLuso.Data.Context;
using DreamLuso.Data.Infrastructure;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Text;


namespace DreamLuso.Data.Uow;

internal class UnitOfWork(ApplicationDbContext context, IUserRepository userRepository,
    ITokenService tokenService, IDataProtectionService dataProtectionService, IAccountRepository accountRepository,
    IAddressRepository addressRepository, IPropertyRepository propertyRepository, IRealStateAgentRepository realStateAgentRepository,
    ICategoryRepository categoryRepository,IClientRepository clientRepository, ICommentRepository commentRepository,
    IContractRepository contractRepository, IFileStorageService fileStorageService, IInvoiceRepository invoiceRepository,
    IPropertyVisitRepository propertyVisitRepository, INotificationRepository notificationRepository, IChatRepository chatRepository, IMessageRepository messageRepository
    ) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;
    //Repository
    public IUserRepository UserRepository => userRepository;
    public IAccountRepository AccountRepository => accountRepository;
    public IPropertyRepository PropertyRepository => propertyRepository;
    public IAddressRepository AddressRepository => addressRepository;
    public IRealStateAgentRepository RealStateAgentRepository => realStateAgentRepository;
    public ICategoryRepository CategoryRepository => categoryRepository;
    public IClientRepository ClientRepository => clientRepository;
    public ICommentRepository CommentRepository => commentRepository;
    public IContractRepository ContractRepository => contractRepository;
    public IFileStorageService FileStorageService => fileStorageService;
    public IInvoiceRepository InvoiceRepository => invoiceRepository;
    public IPropertyVisitRepository PropertyVisitRepository => propertyVisitRepository;
    public INotificationRepository NotificationRepository => notificationRepository;
    public IChatRepository ChatRepository => chatRepository;
    public IMessageRepository MessageRepository => messageRepository;

    //JWT
    public ITokenService TokenService => tokenService;
    public IDataProtectionService DataProtectionService => dataProtectionService;
    


    public bool Commit()
    {
        return context.SaveChanges() > 0;
    }
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {

        return await context.SaveChangesAsync() > 0;
    }

    public IEnumerable<string> DebugChanges()
    {
        var changes = new StringBuilder();

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                changes.AppendLine($"Entity: {entry.Entity.GetType().Name}");
                changes.AppendLine($"State: {entry.State}");

                foreach (var property in entry.OriginalValues.Properties)
                {
                    var originalValue = entry.OriginalValues[property]?.ToString();
                    var currentValue = entry.CurrentValues[property]?.ToString();

                    if (entry.State == EntityState.Added)
                    {
                        changes.AppendLine($"Property: {property.Name} | New Value: {currentValue}");
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        changes.AppendLine($"Property: {property.Name} | Original Value: {originalValue}");
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (originalValue != currentValue)
                        {
                            changes.AppendLine($"Property: {property.Name} | Original Value: {originalValue} | Current Value: {currentValue}");
                        }
                    }
                }
            }
        }

        return changes.ToString().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public bool HasChanges()
    {
        return context.ChangeTracker.HasChanges();
    }
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction.GetDbTransaction();
    }
    public async Task RollbackTrasactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync(cancellationToken);
    }
    public async Task CommitTransactioAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_transaction != null)
            {
                await context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTrasactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            return;
        }
        if (disposing)
        {
            _transaction.Dispose();
            context.Dispose();
        }
        _disposed = true;
    }
    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    ~UnitOfWork()
    {
        Dispose(true);
    }
}
