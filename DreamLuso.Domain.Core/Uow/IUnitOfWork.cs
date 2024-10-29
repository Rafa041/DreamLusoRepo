using DreamLuso.Security.Interfaces;
using System.Data.Common;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IUnitOfWork
{
    //Reposiotory
    public IUserRepository UserRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public IPropertyRepository PropertyRepository{get;}
    public IAddressRepository AddressRepository { get; }
    public IRealStateAgentRepository RealStateAgentRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IClientRepository ClientRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public IContractRepository ContractRepository { get; }
    public IFileStorageService FileStorageService { get; }
    public INotificationRepository NotificationRepository { get; }
    //JWT
    public ITokenService TokenService { get; }
    public IDataProtectionService DataProtectionService { get; }

    //Unit_Of_Work
    bool Commit();

    Task<bool> CommitAsync(CancellationToken cancellationToken = default);

    Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactioAsync(CancellationToken cancellationToken = default);

    Task RollbackTrasactionAsync(CancellationToken cancellationToken = default);

    bool HasChanges();

    IEnumerable<string> DebugChanges();

}


