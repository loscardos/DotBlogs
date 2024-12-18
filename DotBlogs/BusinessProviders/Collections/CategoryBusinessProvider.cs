using DotBlogs.DataProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders.Collections;

public class CategoryBusinessProvider : ICategoryBusinessProvider
{
    private readonly ILogger<CategoryBusinessProvider> _logger;
    private readonly ICategoryDataProvider _dataProvider;

    public CategoryBusinessProvider(
        ILogger<CategoryBusinessProvider> logger,
        ICategoryDataProvider dataProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public async Task<Responses<Category>> Get(CategoryListBindingModel categoryListBindingModel)
    {
        Responses<Category> responses = await _dataProvider.Get(categoryListBindingModel);

        if (responses.Data == null || !responses.Data.Any())
            return new Responses<Category>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ApplicationConstant.NoContentMessage
            };
        
        return responses;
    }

    public async Task<Response<Category>> GetById(string id)
    {
        Category? category = await _dataProvider.GetById(id);

        if (category == null)
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = ApplicationConstant.DataNotFound
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = category
        };
    }

    public async Task<Response<Category>> Create(Category category)
    {
        Category? categoryCreate = await _dataProvider.Create(category);

        if (categoryCreate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = category
        };
    }

    public async Task<Response<Category>> Update(Category category)
    {
        Category? categoryUpdate = await _dataProvider.Update(category);

        if (categoryUpdate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = category
        };
    }

    public async Task<Response<Category>> Delete(string categoryId)
    {
        Category? categoryDelete = await _dataProvider.Delete(categoryId);

        if (categoryDelete == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status204NoContent,
        };
    }
}