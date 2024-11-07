using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamLuso.Data.Repositories;

public class CategoryRepository : PaginatedRepository<Category, Guid>, ICategoryRepository
{
    protected readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Category> GetByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(u => u.Name == name);
    }
}
