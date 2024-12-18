using DotBlogs.Infrastructures;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.EntityFrameworkCore;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders.Collections;

public class PostDataProvider : IPostDataProvider
{
    private readonly DotBlogsContext _context;

    private readonly DbSet<Post> _posts;
    private readonly DbSet<Comment> _comments;

    public PostDataProvider(
        DotBlogsContext context
    )
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _posts = _context.Posts;
        _comments = _context.Comments;
    }

    public async Task<Responses<Post>> Get(PostListBindingModel postListBindingModel)
    {
        var pageSize = postListBindingModel.PageSize ?? 0;
        var currentPage = postListBindingModel.CurrentPage ?? 1;

        List<Post> post;

        IQueryable<Post> query = _posts.AsQueryable()
            .Where(x =>
                string.IsNullOrEmpty(postListBindingModel.Search) ||
                EF.Functions.Like(x.Title!.ToLower(), "%" + postListBindingModel.Search.ToLower() + "%") 
            );

        int totalItem = await query.CountAsync();

        if (pageSize > 0)
        {
            post = await query.Skip(Math.Min(pageSize * (currentPage - 1), totalItem)).Take(pageSize).ToListAsync();
            int totalPages = (int)Math.Ceiling((double)totalItem / pageSize);

            return new()
            {
                StatusCode = StatusCodes.Status200OK,
                Data = post,
                Meta = new()
                {
                    TotalRecords = totalItem,
                    TotalPages = totalPages,
                }
            };
        }

        post = await query.ToListAsync();

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = post,
            Meta = new()
            {
                TotalRecords = totalItem,
                TotalPages = 1
            }
        };
    }

    public async Task<Post?> GetById(string id)
    {
        Post? post = await _posts
            .AsNoTracking()
            .Include(x => x.Author)
            .Include(x => x.Comments)
                .ThenInclude(x => x.Parent)
            .Include(o => o.Category)
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (post == null)
            return null;

        return post;
    }
    
    public async Task<Post?> Create(Post post)
    {
        await _posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> Update(Post post)
    {
        Post? existingPost = await _posts
            .Where(x => x.Id.Equals(post.Id))
            .FirstOrDefaultAsync();

        if (existingPost == null) return null;

        _context.Entry(existingPost).CurrentValues.SetValues(post);

        await _context.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> Delete(string postId)
    {

        Post? postToDelete = await _posts
            .AsNoTracking()
            .Where(x => x.Id.Equals(postId))
            .FirstOrDefaultAsync();
        
        if (postToDelete == null)
            return null;
        
        // use selective lazy
        await _posts.Entry(postToDelete).Collection(p => p.Comments).LoadAsync();
        
        _posts.Remove(postToDelete);
        _comments.RemoveRange(postToDelete.Comments.ToList());

        await _context.SaveChangesAsync();

        return postToDelete;
    }
}