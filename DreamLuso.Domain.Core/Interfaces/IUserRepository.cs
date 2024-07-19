using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;
public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email);
}
