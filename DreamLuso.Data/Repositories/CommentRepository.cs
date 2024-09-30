using DreamLuso.Data.Context;
using DreamLuso.Domain.Core.Interfaces;
using DreamLuso.Domain.Model;

namespace DreamLuso.Data.Repository;

public class CommentRepository : PaginatedRepository<Comment, Guid>, ICommentRepository
{
    protected readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}