using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface IAccountRepository : IRepository<Account, Guid>
{
    Task<Account> GetByEmailAsync(string email);
    Task<Account> GetByUserIdAsync(Guid userId);
}
