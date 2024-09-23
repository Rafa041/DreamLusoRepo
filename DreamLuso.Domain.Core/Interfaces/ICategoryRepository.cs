using DreamLuso.Domain.Model;

namespace DreamLuso.Domain.Core.Interfaces;

public interface ICategoryRepository : IRepository<Category, Guid>
{
    Task<Category> GetByNameAsync(string name);
}
