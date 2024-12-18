using DotBlogs.DataProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders.Collections;

public class PostBusinessProvider : IPostBusinessProvider
{
    private readonly ILogger<PostBusinessProvider> _logger;
    private readonly IPostDataProvider _dataProvider;

    public PostBusinessProvider(
        ILogger<PostBusinessProvider> logger,
        IPostDataProvider dataProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public async Task<Responses<Post>> Get(PostListBindingModel postListBindingModel)
    {
        Responses<Post> responses = await _dataProvider.Get(postListBindingModel);

        if (responses.Data == null || !responses.Data.Any())
            return new Responses<Post>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ApplicationConstant.NoContentMessage
            };
        
        return responses;
    }

    public async Task<Response<Post>> GetById(string id)
    {
        Post? post = await _dataProvider.GetById(id);

        if (post == null)
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = ApplicationConstant.DataNotFound
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = post
        };
    }

    public async Task<Response<Post>> Create(Post post)
    {
        Post? postCreate = await _dataProvider.Create(post);

        if (postCreate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = post
        };
    }

    public async Task<Response<Post>> Update(Post post)
    {
        Post? postUpdate = await _dataProvider.Update(post);

        if (postUpdate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = post
        };
    }

    public async Task<Response<Post>> Delete(string postId)
    {
        Post? postDelete = await _dataProvider.Delete(postId);

        if (postDelete == null)
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