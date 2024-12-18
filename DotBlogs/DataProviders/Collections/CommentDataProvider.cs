using DotBlogs.Infrastructures;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.EntityFrameworkCore;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders.Collections;

public class CommentDataProvider : ICommentDataProvider
{
    private readonly DotBlogsContext _context;

    private readonly DbSet<Comment> _comments;

    public CommentDataProvider(
        DotBlogsContext context
    )
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _comments = _context.Comments;
    }

    public async Task<Responses<Comment>> Get(CommentListBindingModel categoryListBindingModel)
    {
        var pageSize = categoryListBindingModel.PageSize ?? 0;
        var currentPage = categoryListBindingModel.CurrentPage ?? 1;

        List<Comment> category;

        IQueryable<Comment> query = _comments.AsQueryable()
            .Where(x =>
                string.IsNullOrEmpty(categoryListBindingModel.Search) ||
                EF.Functions.Like(x.Content!.ToLower(), "%" + categoryListBindingModel.Search.ToLower() + "%") 
            );

        int totalItem = await query.CountAsync();

        if (pageSize > 0)
        {
            category = await query.Skip(Math.Min(pageSize * (currentPage - 1), totalItem)).Take(pageSize).ToListAsync();
            int totalPages = (int)Math.Ceiling((double)totalItem / pageSize);

            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = category,
                Meta = new()
                {
                    TotalRecords = totalItem,
                    TotalPages = totalPages,
                }
            };
        }

        category = await query.ToListAsync();

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = category,
            Meta = new()
            {
                TotalRecords = totalItem,
                TotalPages = 1
            }
        };
    }

    public async Task<Comment?> GetById(string id)
    {
        Comment? category = await _comments
            .AsNoTracking()
            .Include(x => x.Parent)
            .Include(x => x.Post)
            .Include(x => x.User)
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (category == null)
            return null;

        return category;
    }

    public async Task<Comment?> Create(Comment category)
    {
        await _comments.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }
}