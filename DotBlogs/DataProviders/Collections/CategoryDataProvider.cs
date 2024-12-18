using DotBlogs.Infrastructures;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.EntityFrameworkCore;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders.Collections;

public class CategoryDataProvider : ICategoryDataProvider
{
    private readonly DotBlogsContext _context;

    private readonly DbSet<Category> _categories;

    public CategoryDataProvider(
        DotBlogsContext context
    )
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _categories = _context.Categories;
    }

    public async Task<Responses<Category>> Get(CategoryListBindingModel categoryListBindingModel)
    {
        var pageSize = categoryListBindingModel.PageSize ?? 0;
        var currentPage = categoryListBindingModel.CurrentPage ?? 1;

        List<Category> category;

        IQueryable<Category> query = _categories.AsQueryable()
            .Where(x =>
                string.IsNullOrEmpty(categoryListBindingModel.Search) ||
                EF.Functions.Like(x.Name!.ToLower(), "%" + categoryListBindingModel.Search.ToLower() + "%") 
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

    public async Task<Category?> GetById(string id)
    {
        Category? category = await _categories
            .AsNoTracking()
            .Where(x => x.Id.Equals(id))
            .FirstOrDefaultAsync();

        if (category == null)
            return null;

        return category;
    }

    public async Task<Category?> Create(Category category)
    {
        await _categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> Update(Category category)
    {
        Category? existingCategory = await _categories
            .Where(x => x.Id.Equals(category.Id))
            .FirstOrDefaultAsync();

        if (existingCategory == null) return null;

        _context.Entry(existingCategory).CurrentValues.SetValues(category);

        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> Delete(string categoryId)
    {
        Category? categoryToDelete = await GetById(categoryId);

        if (categoryToDelete == null)
            return null;

        _categories.Remove(categoryToDelete);
        
        await _context.SaveChangesAsync();

        return categoryToDelete;
    }
}